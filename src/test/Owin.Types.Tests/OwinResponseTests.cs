using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Shouldly;
using Xunit;

namespace Owin.Types.Tests
{
    public class OwinResponseTests
    {
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
                    {"owin.ResponseStatusCode", 1},
                    {"owin.ResponseReasonPhrase", "two"},
                    {"owin.ResponseProtocol", "three"},
                    {"owin.ResponseHeaders", headers},
                    {"owin.ResponseBody", body},
                    {"owin.Version", "1.0"},
                    {"owin.CallCancelled", cts.Token},
                };

            var res = new OwinResponse(env);
            res.StatusCode.ShouldBe(1);
            res.ReasonPhrase.ShouldBe("two");
            res.Protocol.ShouldBe("three");
            res.Headers.ShouldBeSameAs(headers);
            res.Body.ShouldBeSameAs(body);
            res.OwinVersion.ShouldBe("1.0");
            res.CallCancelled.ShouldBe(cts.Token);
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
            var res = new OwinResponse(env)
            {
                StatusCode = 1,
                ReasonPhrase = "two",
                Protocol = "three",
                Headers = headers,
                Body = body,
                OwinVersion = "1.0",
                CallCancelled = cts.Token
            };
            env["owin.ResponseStatusCode"].ShouldBe(1);
            env["owin.ResponseReasonPhrase"].ShouldBe("two");
            env["owin.ResponseProtocol"].ShouldBe("three");
            env["owin.ResponseHeaders"].ShouldBe(headers);
            env["owin.ResponseBody"].ShouldBe(body);
            env["owin.Version"].ShouldBe("1.0");
            env["owin.CallCancelled"].ShouldBe(cts.Token);
        }
    }
}
