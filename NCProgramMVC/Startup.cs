using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NCProgramMVC.Startup))]
namespace NCProgramMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
