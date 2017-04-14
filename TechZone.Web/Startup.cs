using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TechZone.Web.Startup))]
namespace TechZone.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
