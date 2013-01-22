using System;
using Owin.Types.Extensions;
using Shouldly;
using Xunit;

namespace Owin.Types.Tests
{
    public class OwinHelpersMethodOverrideTests
    {
        private static OwinRequest Create(Action<OwinRequest> setup)
        {
            var request = OwinRequest.Create();
            setup(request);
            return request;
        }

        [Fact]
        public void RequestMethodUsedIfAvailable()
        {
            Create(req => req.Method = "one")
                .GetMethodOverride()
                .ShouldBe("one");
            Create(req => req.Method = "GET")
                .GetMethodOverride()
                .ShouldBe("GET");
            Create(req => req.Method = "POST")
                .GetMethodOverride()
                .ShouldBe("POST");
            Create(req => req.Method = "PUT")
                .GetMethodOverride()
                .ShouldBe("PUT");
        }

        [Fact]
        public void MethodOverrideReplacesPostMethod()
        {
            Create(req => req
                .SetHeader("X-Http-Method-Override", "PUT")
                .Set(OwinConstants.RequestMethod, "POST"))
                .GetMethodOverride()
                .ShouldBe("PUT");
            Create(req => req
                .SetHeader("X-Http-Method-Override", "GET")
                .Set(OwinConstants.RequestMethod, "POST"))
                .GetMethodOverride()
                .ShouldBe("GET");
            Create(req => req
                .SetHeader("X-Http-Method-Override", "x")
                .Set(OwinConstants.RequestMethod, "POST"))
                .GetMethodOverride()
                .ShouldBe("x");
        }

        [Fact]
        public void MethodDoesNotReplaceOtherMethods()
        {
            Create(req => req
                .SetHeader("X-Http-Method-Override", "x")
                .Set(OwinConstants.RequestMethod, "PUT"))
                .GetMethodOverride()
                .ShouldBe("PUT");
            Create(req => req
                .SetHeader("X-Http-Method-Override", "x")
                .Set(OwinConstants.RequestMethod, "GET"))
                .GetMethodOverride()
                .ShouldBe("GET");
            Create(req => req
                .SetHeader("X-Http-Method-Override", "x")
                .Set(OwinConstants.RequestMethod, "CUSTOM"))
                .GetMethodOverride()
                .ShouldBe("CUSTOM");
        }

        [Fact]
        public void ApplyMethodOverrideChangesMethodOnlyWhenPost()
        {
            Create(req => req
                .SetHeader("X-Http-Method-Override", "PUT")
                .Set(OwinConstants.RequestMethod, "POST"))
                .ApplyMethodOverride()
                .Method.ShouldBe("PUT");
            Create(req => req
                .SetHeader("X-Http-Method-Override", "GET")
                .Set(OwinConstants.RequestMethod, "POST"))
                .ApplyMethodOverride()
                .Method.ShouldBe("GET");
            Create(req => req
                .SetHeader("X-Http-Method-Override", "x")
                .Set(OwinConstants.RequestMethod, "POST"))
                .ApplyMethodOverride()
                .Method.ShouldBe("x");
            Create(req => req
                .SetHeader("X-Http-Method-Override", "x")
                .Set(OwinConstants.RequestMethod, "PUT"))
                .ApplyMethodOverride()
                .Method.ShouldBe("PUT");
            Create(req => req
                .SetHeader("X-Http-Method-Override", "x")
                .Set(OwinConstants.RequestMethod, "GET"))
                .ApplyMethodOverride()
                .Method.ShouldBe("GET");
            Create(req => req
                .SetHeader("X-Http-Method-Override", "x")
                .Set(OwinConstants.RequestMethod, "CUSTOM"))
                .ApplyMethodOverride()
                .Method.ShouldBe("CUSTOM");
        }
    }
}
