using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
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
                if (role.Equals("Staff"))
                {
                    return View();
                }
            }
            return RedirectToAction("Unauthorize","Login");
        }
    }
}