using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AngularAssignment.Startup))]
namespace AngularAssignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
