using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Austumate.Startup))]
namespace Austumate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app); 
        }
    }
}
