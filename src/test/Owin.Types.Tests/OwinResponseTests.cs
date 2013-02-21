// <copyright file="OwinResponseTests.cs" company="Microsoft Open Technologies, Inc.">
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
                    { "alpha", new[] { "beta", "gamma" } }
                };
            var body = new MemoryStream(new byte[] { 65, 66, 67, 68 });
            var cts = new CancellationTokenSource();
            var env = new Dictionary<string, object>(StringComparer.Ordinal)
                {
                    { "owin.ResponseStatusCode", 1 },
                    { "owin.ResponseReasonPhrase", "two" },
                    { "owin.ResponseProtocol", "three" },
                    { "owin.ResponseHeaders", headers },
                    { "owin.ResponseBody", body },
                    { "owin.Version", "1.0" },
                    { "owin.CallCancelled", cts.Token },
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
                    { "alpha", new[] { "beta", "gamma" } }
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

        [Fact]
        public void WritesWork()
        {
            var body = new MemoryStream(4);
            var expectedBody = new byte[] { 65, 66, 67, 68 };
            var cts = new CancellationTokenSource();
            var env = new Dictionary<string, object>(StringComparer.Ordinal)
                {
                    { "owin.CallCancelled", cts.Token },
                    { "owin.ResponseBody", body },
                };

            var res = new OwinResponse(env);
            res.Write("AB");
            res.WriteAsync("CD").Wait();

            body.GetBuffer().ShouldBe(expectedBody);
            res.CallCancelled.ShouldBe(cts.Token);
        }

        [Fact]
        public void ContentTypeSetsOrRemovesAppropriateHeader()
        {
            var request = OwinRequest.Create();
            var response = new OwinResponse(request);
            response.Headers.ContainsKey("Content-Type").ShouldBe(false);
            response.ContentType.ShouldBe(null);
            response.ContentType = "text/plain";
            response.Headers["Content-Type"].ShouldBe(new[] { "text/plain" });
            response.ContentType.ShouldBe("text/plain");
            response.ContentType = null;
            response.Headers.ContainsKey("Content-Type").ShouldBe(false);
            response.ContentType.ShouldBe(null);
        }
    }
}
