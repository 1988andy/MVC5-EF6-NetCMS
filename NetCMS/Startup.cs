using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetCMS.Startup))]
namespace NetCMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
