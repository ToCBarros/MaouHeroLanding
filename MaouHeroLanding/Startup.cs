using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MaouHeroLanding.Startup))]
namespace MaouHeroLanding
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
