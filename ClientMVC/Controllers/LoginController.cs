using API_RBAC.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClientMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        public LoginController()
        {
            client.BaseAddress = new Uri("http://localhost:50693/");
            client.DefaultRequestHeaders.Accept.Clear();
        }
        // GET: Login/
        public ActionResult Index()
        {
            return View();
        }

        //POST : Login/
        [HttpPost]
        public async Task<JsonResult> Login(UserVM userVM)
        {
            var myContent = JsonConvert.SerializeObject(userVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync("GenerateToken", byteContent);
            if (result.IsSuccessStatusCode)
            {
                var Token = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                //var handler = new JwtSecurityTokenHandler();
                //var decodedToken = handler.ReadJwtToken(theToken);
                //var hasilRole = decodedToken.Claims.Where(p => p.Type == "Role").FirstOrDefault()?.Value;
                Session["Token"] = Token;
                if (Token != null)
                {
                    return Json("200", JsonRequestBehavior.AllowGet);
                }
                //return Json(new {role = hasilRole }, JsonRequestBehavior.AllowGet);
            }
            return Json("400", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Unauthorize()
        {
            return View();
        }
        
        public JsonResult Logout()
        {
            Session.Clear();
            return Json("clear");
        }
    }
}