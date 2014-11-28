using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nmct.ba.cashlessproject.webservice.Startup))]
namespace nmct.ba.cashlessproject.webservice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
