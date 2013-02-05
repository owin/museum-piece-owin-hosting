using System;
using Owin.Types.Extensions;
using Shouldly;
using Xunit;

namespace Owin.Types.Tests
{
    public class OwinHelpersForwardedTests
    {
        private static OwinRequest Create(Action<OwinRequest> setup)
        {
            var request = OwinRequest.Create();
            setup(request);
            return request;
        }

        [Fact]
        public void RequestSchemeUsedIfAvailable()
        {
            Create(req => req.Scheme = "one")
                .GetForwardedScheme()
                .ShouldBe("one");
        }

        [Fact]
        public void ForwardedSslOnImpliesHttps()
        {
            Create(req => req.SetHeader("X-Forwarded-Ssl", "on").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("https");
            Create(req => req.SetHeader("X-Forwarded-Ssl", "off").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("x");
            Create(req => req.SetHeader("X-Forwarded-Ssl", "unknown").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("x");
            Create(req => req.SetHeader("X-Forwarded-Ssl", "").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("x");
        }

        [Fact]
        public void ForwardedSchemeImpliesScheme()
        {
            Create(req => req.SetHeader("X-Forwarded-Scheme", "https").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("https");
            Create(req => req.SetHeader("X-Forwarded-Scheme", "http").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("http");
            Create(req => req.SetHeader("X-Forwarded-Scheme", "unknown").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("unknown");
            Create(req => req.SetHeader("X-Forwarded-Scheme", "").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("x");
        }

        [Fact]
        public void ForwardedProtoImpliesScheme()
        {
            Create(req => req.SetHeader("X-Forwarded-Proto", "https").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("https");
            Create(req => req.SetHeader("X-Forwarded-Proto", "http").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("http");
            Create(req => req.SetHeader("X-Forwarded-Proto", "unknown").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("unknown");
            Create(req => req.SetHeader("X-Forwarded-Proto", "").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("x");
            Create(req => req.SetHeader("X-Forwarded-Proto", "one,two").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("one");
            Create(req => req.SetHeaderUnmodified("X-Forwarded-Proto", "one", "two").Set(OwinConstants.RequestScheme, "x"))
                .GetForwardedScheme()
                .ShouldBe("one");
        }

        [Fact]
        public void ForwardedHostDefaultsToHostHeader()
        {
            Create(req => req.SetHeader("Host", "x"))
                .GetForwardedHost()
                .ShouldBe("x");
            Create(req => req.SetHeader("Host", "one:2"))
                .GetForwardedHost()
                .ShouldBe("one:2");
        }

        [Fact]
        public void ForwardedHostOverridesHostHeader()
        {
            Create(req => req.SetHeader("X-Forwarded-Host", "y").SetHeader("Host", "x"))
                .GetForwardedHost()
                .ShouldBe("y");
            Create(req => req.SetHeader("X-Forwarded-Host", "y:3").SetHeader("Host", "one:2"))
                .GetForwardedHost()
                .ShouldBe("y:3");
            Create(req => req.SetHeader("X-Forwarded-Host", "y:3,z:4").SetHeader("Host", "one:2"))
                .GetForwardedHost()
                .ShouldBe("z:4");
            Create(req => req.SetHeaderUnmodified("X-Forwarded-Host", "y:3", "w:4,z:4").SetHeader("Host", "one:2"))
                .GetForwardedHost()
                .ShouldBe("z:4");
        }

        [Fact]
        public void ForwardedHostAndSchemeAffectForwardedUriOnly()
        {
            var request = Create(req => req
                .Set(OwinConstants.RequestScheme, "one")
                .SetHeader("Host", "two")
                .SetHeader("x-Forwarded-Scheme", "x")
                .SetHeader("X-Forwarded-Host", "y")
                .Set(OwinConstants.RequestPathBase, "/"));
            request.Uri.ShouldBe(new Uri("one://two/"));
            request.GetForwardedUri().ShouldBe(new Uri("x://y/"));
        }

        [Fact]
        public void ApplyForwardedSchemeAffectsOnlyUriScheme()
        {
            var request = Create(req => req
                .Set(OwinConstants.RequestScheme, "one")
                .SetHeader("Host", "two:3")
                .SetHeader("x-Forwarded-Scheme", "x")
                .SetHeader("X-Forwarded-Host", "y:4")
                .Set(OwinConstants.RequestPathBase, "/"));

            request.Uri.ShouldBe(new Uri("one://two:3/"));
            request.GetForwardedUri().ShouldBe(new Uri("x://y:4/"));
            request.ApplyForwardedScheme();
            request.Uri.ShouldBe(new Uri("x://two:3/"));
            request.GetForwardedUri().ShouldBe(new Uri("x://y:4/"));
        }

        [Fact]
        public void ApplyForwardedHostAffectsOnlyUriScheme()
        {
            var request = Create(req => req
                .Set(OwinConstants.RequestScheme, "one")
                .SetHeader("Host", "two:3")
                .SetHeader("x-Forwarded-Scheme", "x")
                .SetHeader("X-Forwarded-Host", "y:4")
                .Set(OwinConstants.RequestPathBase, "/"));

            request.Uri.ShouldBe(new Uri("one://two:3/"));
            request.GetForwardedUri().ShouldBe(new Uri("x://y:4/"));
            request.ApplyForwardedHost();
            request.Uri.ShouldBe(new Uri("one://y:4/"));
            request.GetForwardedUri().ShouldBe(new Uri("x://y:4/"));
        }

        [Fact]
        public void ApplyForwardedUriAffectsHostAndScheme()
        {
            var request = Create(req => req
                .Set(OwinConstants.RequestScheme, "one")
                .SetHeader("Host", "two:3")
                .SetHeader("x-Forwarded-Scheme", "x")
                .SetHeader("X-Forwarded-Host", "y:4")
                .Set(OwinConstants.RequestPathBase, "/"));

            request.Uri.ShouldBe(new Uri("one://two:3/"));
            request.GetForwardedUri().ShouldBe(new Uri("x://y:4/"));
            request.ApplyForwardedUri();
            request.Uri.ShouldBe(new Uri("x://y:4/"));
            request.GetForwardedUri().ShouldBe(new Uri("x://y:4/"));
        }
    }
}
