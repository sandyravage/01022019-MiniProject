using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MiniProject.Startup))]
namespace MiniProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
