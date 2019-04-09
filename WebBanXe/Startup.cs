using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBanXe.Startup))]
namespace WebBanXe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
