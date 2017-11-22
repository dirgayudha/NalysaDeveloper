using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NalysaDeveloper.Startup))]
namespace NalysaDeveloper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
