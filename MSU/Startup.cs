using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MSU.Startup))]
namespace MSU
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
