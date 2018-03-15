using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Proyecto.Startup))]
namespace Proyecto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
