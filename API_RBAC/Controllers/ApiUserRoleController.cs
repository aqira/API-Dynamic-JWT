using API_RBAC.Context;
using API_RBAC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace API_RBAC.Controllers
{
    [Authorize]
    public class ApiUserRoleController : ApiController
    {
        MyContext db = new MyContext();
        readonly HttpClient client = new HttpClient();

        public ApiUserRoleController()
        {
            client.DefaultRequestHeaders.Add("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI1NDYxYjM0NC05OWY4LTQ4YzgtYThlZS04YmVlZjlkZjFiNTIiLCJJZCI6IjEiLCJOYW1lIjoiYXFpcmFAZ21haWwuY29tIiwiRW1haWwiOiJhcWlyYUBnbWFpbC5jb20iLCJSb2xlIjoiTWFuYWdlciIsImV4cCI6MTYxODU1OTI1MiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDY5My8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUyMDQ5LyJ9.K-Hk-zUOOuAc3_mRpY7WNuPp19djWFVyqe77ioe8aLU");
        }

        public async Task<IHttpActionResult> Get()
        {
            var get = await db.Database.SqlQuery<EmailRoleVM>("SP_Retrieve_AspNetUserRoles").ToListAsync();
            if (get.Count == 0)
            {
                return NotFound();
            }
            return Ok(new {data = get});
        }

        public async Task<IHttpActionResult> Post(UserIdRoleIdVM urVM)
        {
            if (urVM == null || string.IsNullOrWhiteSpace(urVM.UserId) || string.IsNullOrWhiteSpace(urVM.RoleId))
            {
                return BadRequest("The data you inserted is incomplete");
            }
            if (urVM != null)
            {

                SqlParameter paramUserId = new SqlParameter("@UserId", urVM.UserId);
                SqlParameter paramRoleId = new SqlParameter("@RoleId", urVM.RoleId);
                await db.Database.ExecuteSqlCommandAsync("SP_Save_AspNetUserRoles @UserId, @RoleId", paramUserId, paramRoleId);

                return Ok("Ok");
            }

            return Content(HttpStatusCode.InternalServerError, "Unable to Save Changes");

        }
    }
}
