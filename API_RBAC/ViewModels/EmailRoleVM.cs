using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_RBAC.ViewModels
{
    public class EmailRoleVM
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }

    public class EmailRoleJson
    {
        [JsonProperty("data")]
        public List<EmailRoleVM> data { get; set; }
    }

    public class UserIdRoleIdVM
    {
        public string UserId{ get; set; }
        public string RoleId { get; set; }
    }

}