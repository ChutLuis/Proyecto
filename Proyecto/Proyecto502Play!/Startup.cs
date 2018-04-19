using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Proyecto502Play_.Startup))]
namespace Proyecto502Play_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
