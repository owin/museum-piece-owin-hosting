using System;
using Owin;
using Owin.AutoStartup;

namespace $rootnamespace$
{
    public partial class Startup
    {
        public IServiceProvider ServiceProvider { get; set; }

        public Startup()
        {
        }

        public Startup(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public void AutoConfiguration(IAppBuilder app)
        {
            AutoStartupInfrastructure.ExecuteConfigurationMethods(this, app);
        }

        public void Post050_ExecuteOwinAutoStartups(IAppBuilder app)
        {
            AutoStartupInfrastructure.ExecuteOwinAutoStartups(ServiceProvider, app);
        }
    }
}
