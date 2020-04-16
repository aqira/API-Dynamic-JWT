using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_RBAC.ViewModels
{
    public class UserRoleVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}