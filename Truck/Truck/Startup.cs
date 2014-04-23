using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Truck.Startup))]
namespace Truck
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
