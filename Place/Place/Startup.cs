using Microsoft.Owin;
using Owin;
using Place.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Place.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
