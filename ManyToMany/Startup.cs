using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManyToMany.Startup))]
namespace ManyToMany
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
