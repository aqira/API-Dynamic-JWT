using API_RBAC.Context;
using API_RBAC.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http;

namespace API_RBAC.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        MyContext db = new MyContext();

        readonly HttpClient client = new HttpClient();
        public AccountController()
        {
            client.DefaultRequestHeaders.Add("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI1NDYxYjM0NC05OWY4LTQ4YzgtYThlZS04YmVlZjlkZjFiNTIiLCJJZCI6IjEiLCJOYW1lIjoiYXFpcmFAZ21haWwuY29tIiwiRW1haWwiOiJhcWlyYUBnbWFpbC5jb20iLCJSb2xlIjoiTWFuYWdlciIsImV4cCI6MTYxODU1OTI1MiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDY5My8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUyMDQ5LyJ9.K-Hk-zUOOuAc3_mRpY7WNuPp19djWFVyqe77ioe8aLU");
        }

        [Route("GenerateToken")]
        [HttpPost]
        public IHttpActionResult Login(UserVM userVM)
        {
            SqlParameter paramEmail = new SqlParameter("@Email", userVM.Email);
            SqlParameter paramPassword = new SqlParameter("@Password", userVM.Password);
            var getBy = db.Database.SqlQuery<UserRoleVM>("SP_Retrieve_AspNetUsers_TokenMapping @Email, @Password", paramEmail, paramPassword).Single();
            if (getBy != null)
            {
                var result = GenerateToken(getBy);
                return Ok(result);
            }
            return NotFound();
        }

        public string GenerateToken(UserRoleVM userRoleVM)
        {
            string key = "my_secret_key_12345";
            var issuer = "http://localhost:50693/";
            var audience = "http://localhost:52049/";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("Id", userRoleVM.Id));
            permClaims.Add(new Claim("Name", userRoleVM.Name));
            permClaims.Add(new Claim("Email", userRoleVM.Email));
            permClaims.Add(new Claim("Role", userRoleVM.Role));

            var token = new JwtSecurityToken(issuer,
                            audience,
                            permClaims,
                            expires: DateTime.Now.AddYears(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }




        [Route("GetName1")]
        [HttpPost]
        public String GetName1()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                }
                return "Valid";
            }
            else
            {
                return "Invalid";
            }
        }

        [Route("GetName2")]
        [Authorize]
        [HttpPost]
        public Object GetName2()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                var role = claims.Where(p => p.Type == "Role").FirstOrDefault()?.Value;
                return new
                {
                    data = role
                };

            }
            return null;
        }
    }
}