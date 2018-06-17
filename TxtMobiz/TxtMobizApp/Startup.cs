using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TxtMobizApp.Startup))]
namespace TxtMobizApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
