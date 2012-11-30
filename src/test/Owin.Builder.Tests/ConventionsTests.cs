using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Owin.Builder.Tests
{
    public class ConventionsTests
    {
        [Fact]
        public void StartupConvetion1Success()
        {
            IAppBuilder builder = new AppBuilder();
            new StartupConvention1.Startup().Configuration(builder);
            builder.Build();
        }

        [Fact]
        public void StartupConvetion2Success()
        {
            IAppBuilder builder = new AppBuilder();
            new StartupConvention2.Startup().Configuration(builder.Properties);
        }
    }
}
