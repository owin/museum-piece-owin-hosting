using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Owin.Types.Tests
{
    public class StartupExtensionsTests
    {
        [Fact]
        public void AnonymousLambdaInUnambiguousForEachExtensionMethod()
        {
            IAppBuilder app = new StubBuilder();
            app.UseFilter(request => { });
            app.UseFilter(async request => { await Task.Delay(0); });
            app.UseHandler((request, response) => { });
            app.UseHandler(async (request, response) => { await Task.Delay(0); });
            app.UseHandler(async (request, response, next) => { await next(); });
        }

        public class StubBuilder : IAppBuilder
        {
            public IAppBuilder Use(object middleware, params object[] args)
            {
                return this;
            }

            public object Build(Type returnType)
            {
                return null;
            }

            public IAppBuilder New()
            {
                return new StubBuilder();
            }

            public IDictionary<string, object> Properties { get; protected set; }
        }
    }
}
