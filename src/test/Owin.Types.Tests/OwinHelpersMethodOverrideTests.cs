// <copyright file="OwinHelpersMethodOverrideTests.cs" company="Microsoft Open Technologies, Inc.">
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
