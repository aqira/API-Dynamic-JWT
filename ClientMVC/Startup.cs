using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClientMVC.Startup))]
namespace ClientMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
