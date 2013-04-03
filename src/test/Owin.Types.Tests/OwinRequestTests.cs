// <copyright file="OwinRequestTests.cs" company="Microsoft Open Technologies, Inc.">
// Copyright 2013 Microsoft Open Technologies, Inc. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Owin.Types.Extensions;
using Shouldly;
using Xunit;

namespace Owin.Types.Tests
{
    public class OwinRequestTests
    {
        [Fact]
        public void ItCanCreateEnvironmentForTestConvenience()
        {
            var req = OwinRequest.Create();
            req.Dictionary.ShouldNotBe(null);
        }

        [Fact]
        public void ItStronglyTypesOwinKeys()
        {
            var headers = new Dictionary<string, string[]>(StringComparer.InvariantCultureIgnoreCase)
                {
                    { "alpha", new[] { "beta", "gamma" } }
                };

            var body = new MemoryStream(new byte[] { 65, 66, 67, 68 });
            var cts = new CancellationTokenSource();
            var env = new Dictionary<string, object>(StringComparer.Ordinal)
                {
                    { "owin.RequestScheme", "http" },
                    { "owin.RequestMethod", "ONE" },
                    { "owin.RequestPathBase", "/two" },
                    { "owin.RequestPath", "/three" },
                    { "owin.RequestQueryString", "four=five" },
                    { "owin.RequestProtocol", "HTTP/1.0" },
                    { "owin.RequestHeaders", headers },
                    { "owin.RequestBody", body },
                    { "owin.Version", "1.0" },
                    { "owin.CallCancelled", cts.Token },
                };

            var req = new OwinRequest(env);
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
                    { "alpha", new[] { "beta", "gamma" } }
                };
            var body = new MemoryStream(new byte[] { 65, 66, 67, 68 });
            var cts = new CancellationTokenSource();

            var env = new Dictionary<string, object>(StringComparer.Ordinal);
            var req = new OwinRequest(env)
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

        private static OwinRequest Create(Action<OwinRequest> setup)
        {
            var request = OwinRequest.Create();
            setup(request);
            return request;
        }

        [Fact]
        public void UriCombinedFromAppropriateFields()
        {
            Create(
                req => req
                    .Set(OwinConstants.RequestScheme, "https")
                    .SetHeader("Host", "two:3")
                    .Set(OwinConstants.RequestPathBase, "/four")
                    .Set(OwinConstants.RequestPath, "/five")
                    .Set(OwinConstants.RequestQueryString, "six=7"))
                .Uri.ShouldBe(new Uri("https://two:3/four/five?six=7"));
        }

        [Fact]
        public void MissingHostDefaultsLocalIpAddressAndPort()
        {
            Create(
                req => req
                    .Set(OwinConstants.RequestScheme, "https")
                    .Set(OwinConstants.CommonKeys.LocalIpAddress, "eight")
                    .Set(OwinConstants.CommonKeys.LocalPort, "9")
                    .Set(OwinConstants.RequestPathBase, "/four")
                    .Set(OwinConstants.RequestPath, "/five")
                    .Set(OwinConstants.RequestQueryString, "six=7"))
                .Uri.ShouldBe(new Uri("https://eight:9/four/five?six=7"));
        }

        [Fact]
        public void HostMissingPortDoesNotDefaultToLocalPort()
        {
            Create(
                req => req
                    .Set(OwinConstants.RequestScheme, "https")
                    .SetHeader("Host", "two")
                    .Set(OwinConstants.CommonKeys.LocalIpAddress, "eight")
                    .Set(OwinConstants.CommonKeys.LocalPort, "9")
                    .Set(OwinConstants.RequestPathBase, "/four")
                    .Set(OwinConstants.RequestPath, "/five")
                    .Set(OwinConstants.RequestQueryString, "six=7"))
                .Uri.ShouldBe(new Uri("https://two/four/five?six=7"));
        }

        [Fact]
        public void CookiesCanBeParsed()
        {
            Create(
                req => req.SetHeader("Cookie", "a=1;b=2"))
                .GetCookies().ShouldBe(new Dictionary<string, string> { { "a", "1" }, { "b", "2" } });
        }

        [Fact]
        public void CookieDelimitersCanBeFollowedWithSpace()
        {
            Create(
                req => req.SetHeader("Cookie", "a=1; b=2"))
                .GetCookies().ShouldBe(new Dictionary<string, string> { { "a", "1" }, { "b", "2" } });
        }

        [Fact]
        public void OnlyTheFirstInstanceOfACookieIsSignificant()
        {
            Create(
                req => req.SetHeader("Cookie", "a=1; b=2; a=3; c=4"))
                .GetCookies().ShouldBe(new Dictionary<string, string> { { "a", "1" }, { "b", "2" }, { "c", "4" } });
        }

        [Fact]
        public void QueryCanBeParsedToDictionary()
        {
            Create(
                req => req.QueryString = "a=1&b=2")
                .GetQuery().ShouldBe(new Dictionary<string, string[]> { { "a", new[] { "1" } }, { "b", new[] { "2" } } });
        }

        [Fact]
        public void MultipleOccurencesWillBecomeArrayed()
        {
            Create(
                req => req.QueryString = "a=1&b=2&a=3")
                .GetQuery().ShouldBe(new Dictionary<string, string[]> { { "a", new[] { "1", "3" } }, { "b", new[] { "2" } } });
        }
    }
}
