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
using System.Web.Mvc;

namespace API_RBAC.Controllers
{
    public class ApiUserRoleController : ApiController
    {
        MyContext db = new MyContext();

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
