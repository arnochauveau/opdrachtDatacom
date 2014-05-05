using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(angulartest.Startup))]
namespace angulartest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
