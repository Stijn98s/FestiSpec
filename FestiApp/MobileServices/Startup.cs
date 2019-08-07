using System;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FestiMS.Startup))]

namespace FestiMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}