using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleThx.webmvc.Startup))]
namespace SimpleThx.webmvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }
    }
}
