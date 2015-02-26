using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClothItWeb.Startup))]
namespace ClothItWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
