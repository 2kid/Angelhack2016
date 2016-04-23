using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AngelHack2016.Startup))]
namespace AngelHack2016
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
