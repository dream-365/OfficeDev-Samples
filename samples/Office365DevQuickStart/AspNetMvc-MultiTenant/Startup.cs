using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspNetMvc_MultiTenant.Startup))]
namespace AspNetMvc_MultiTenant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}