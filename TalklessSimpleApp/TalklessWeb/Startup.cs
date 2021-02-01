using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TalklessWeb.Startup))]
namespace TalklessWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
