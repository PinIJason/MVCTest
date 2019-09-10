using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebMVC2015.Startup))]
namespace WebMVC2015
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
