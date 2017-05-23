using Microsoft.Owin;
using Owin;
using Petrovich.Web.Security;

[assembly: OwinStartupAttribute(typeof(Petrovich.Web.Startup))]
namespace Petrovich.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AuthenticationConfiguration.ConfigureAuth(app);
            AuthenticationConfiguration.CreateDefaultArtifacts(app);
        }
    }
}
