using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BPropertyManagement.Startup))]
namespace BPropertyManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
