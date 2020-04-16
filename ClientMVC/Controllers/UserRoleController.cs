using API_RBAC.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ClientMVC.Controllers
{
    public class UserRoleController : Controller
    {

        // GET: UserRole
        public ActionResult Index()
        {
            var ses = Session["Token"];
            if (ses == null)
            {
                return RedirectToAction("Unauthorize", "Login");
            }
            string token = ses.ToString();
            if (token != null)
            {
                var role = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.Where(p => p.Type == "Role").FirstOrDefault()?.Value;
                if (role.Equals("Manager"))
                {
                    return View();
                }
            }
            return RedirectToAction("Unauthorize", "Login");
        }

        public JsonResult LoadData()
        {
            EmailRoleJson emailRoleJson = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:50693/api/")
            };
            var responseTask = client.GetAsync("apiUserRole");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                string json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                emailRoleJson = JsonConvert.DeserializeObject<EmailRoleJson>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(emailRoleJson, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult SaveData(UserIdRoleIdVM userIdRoleIdVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:50693/api/")
            };
            var myContent = JsonConvert.SerializeObject(userIdRoleIdVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("apiUserRole/", byteContent).Result;
            if (result.IsSuccessStatusCode == true)
            {
                return Json(new { value = "Ok"}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { value = "Error" });
        }
    }
}