using Microsoft.Owin;
using Owin;
using rampsnamp.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace rampsnamp.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}