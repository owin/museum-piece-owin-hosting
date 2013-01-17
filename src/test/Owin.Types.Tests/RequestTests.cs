using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Shouldly;
using Xunit;

namespace Owin.Types.Tests
{
    public class RequestTests
    {
        [Fact]
        public void ItCanCreateEnvironmentForTestConvenience()
        {
            var req = Request.Create();
            req.Environment.ShouldNotBe(null);
        }

        [Fact]
        public void ItStronglyTypesOwinKeys()
        {
            var headers = new Dictionary<string, string[]>(StringComparer.InvariantCultureIgnoreCase)
                {
                    {"alpha", new []{"beta", "gamma"}}
                };
            var body = new MemoryStream(new byte[] { 65, 66, 67, 68 });
            var cts = new CancellationTokenSource();
            var env = new Dictionary<string, object>(StringComparer.Ordinal)
                {
                    {"owin.RequestScheme", "http"},
                    {"owin.RequestMethod", "ONE"},
                    {"owin.RequestPathBase", "/two"},
                    {"owin.RequestPath", "/three"},
                    {"owin.RequestQueryString", "four=five"},
                    {"owin.RequestProtocol", "HTTP/1.0"},
                    {"owin.RequestHeaders", headers},
                    {"owin.RequestBody", body},
                    {"owin.Version", "1.0"},
                    {"owin.CallCancelled", cts.Token},
                };

            var req = new Request(env);
            req.Scheme.ShouldBe("http");
            req.Method.ShouldBe("ONE");
            req.PathBase.ShouldBe("/two");
            req.Path.ShouldBe("/three");
            req.QueryString.ShouldBe("four=five");
            req.Protocol.ShouldBe("HTTP/1.0");
            req.Headers.ShouldBeSameAs(headers);
            req.Body.ShouldBeSameAs(body);
            req.OwinVersion.ShouldBe("1.0");
            req.CallCancelled.ShouldBe(cts.Token);
        }

        [Fact]
        public void SettersModifyEnvironment()
        {
            var headers = new Dictionary<string, string[]>(StringComparer.InvariantCultureIgnoreCase)
                {
                    {"alpha", new []{"beta", "gamma"}}
                };
            var body = new MemoryStream(new byte[] { 65, 66, 67, 68 });
            var cts = new CancellationTokenSource();

            var env = new Dictionary<string, object>(StringComparer.Ordinal);
            var req = new Request(env)
                {
                    Scheme = "http",
                    Method = "ONE",
                    PathBase = "/two",
                    Path = "/three",
                    QueryString = "four=five",
                    Protocol = "HTTP/1.0",
                    Headers = headers,
                    Body = body,
                    OwinVersion = "1.0",
                    CallCancelled = cts.Token
                };
            env["owin.RequestScheme"].ShouldBe("http");
            env["owin.RequestMethod"].ShouldBe("ONE");
            env["owin.RequestPathBase"].ShouldBe("/two");
            env["owin.RequestPath"].ShouldBe("/three");
            env["owin.RequestQueryString"].ShouldBe("four=five");
            env["owin.RequestProtocol"].ShouldBe("HTTP/1.0");
            env["owin.RequestHeaders"].ShouldBe(headers);
            env["owin.RequestBody"].ShouldBe(body);
            env["owin.Version"].ShouldBe("1.0");
            env["owin.CallCancelled"].ShouldBe(cts.Token);
        }
    }
}
