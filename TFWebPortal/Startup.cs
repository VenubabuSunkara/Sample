using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TransForce.Web.Portal.Startup))]
namespace TransForce.Web.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
