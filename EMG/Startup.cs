using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EMG.Startup))]
namespace EMG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
