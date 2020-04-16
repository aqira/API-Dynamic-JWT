using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using API_RBAC.Models;

namespace API_RBAC.Context
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class MyContext: IdentityDbContext<ApplicationUser>
    {
        public MyContext() : base("MyConnection", throwIfV1Schema: false)
        {
        }

        public static MyContext Create()
        {
            return new MyContext();
        }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<Orderline> Orderlines { set; get; }
        public DbSet<Product> Products { set; get; }
    }
}