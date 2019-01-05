using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPlaceProject.Startup))]
namespace MyPlaceProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
