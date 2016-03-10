using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApp_with_OpenConnectId.Startup))]
namespace WebApp_with_OpenConnectId
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
