using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImageDrive.Startup))]
namespace ImageDrive
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
