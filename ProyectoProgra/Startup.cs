using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoProgra.Startup))]
namespace ProyectoProgra
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
