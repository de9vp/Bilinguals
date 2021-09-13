using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bilinguals.Startup))]
namespace Bilinguals
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
