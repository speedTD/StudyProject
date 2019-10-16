using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Study.Startup))]
namespace Study
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
