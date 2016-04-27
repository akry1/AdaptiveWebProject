using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdaptiveWebProject.Startup))]
namespace AdaptiveWebProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
