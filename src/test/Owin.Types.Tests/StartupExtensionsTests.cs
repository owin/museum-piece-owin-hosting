// <copyright file="StartupExtensionsTests.cs" company="Microsoft Open Technologies, Inc.">
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
using System.Threading.Tasks;
using Xunit;

namespace Owin.Types.Tests
{
    public class StartupExtensionsTests
    {
        [Fact]
        public void AnonymousLambdaIsUnambiguousForEachExtensionMethod()
        {
            IAppBuilder app = new StubBuilder();
            app.UseFilter(request => { });
            app.UseFilter(async request => { await Task.Delay(0); });
            app.UseHandler((request, response) => { });
            app.UseHandler(async (request, response) => { await Task.Delay(0); });
            app.UseHandler(async (request, response, next) => { await next(); });
        }

        [Fact]
        public void MethodGroupIsUnambiguousForEachExtensionMethod()
        {
            IAppBuilder app = new StubBuilder();
            app.UseFilter((Action<OwinRequest>)OnFilter1);
            app.UseFilterAsync(OnFilter2);
            app.UseHandler((Action<OwinRequest, OwinResponse>)OnHandler1);
            app.UseHandlerAsync(OnHandler2);
            app.UseHandlerAsync(OnHandler3);
        }

        private void OnFilter1(OwinRequest request)
        {
        }

        private async Task OnFilter2(OwinRequest request)
        {
            await Task.Delay(0);
        }

        private void OnHandler1(OwinRequest request, OwinResponse response)
        {
        }

        private async Task OnHandler2(OwinRequest request, OwinResponse response)
        {
            await Task.Delay(0);
        }

        private async Task OnHandler3(OwinRequest request, OwinResponse response, Func<Task> next)
        {
            await next();
        }

        public class StubBuilder : IAppBuilder
        {
            public IDictionary<string, object> Properties { get; protected set; }

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
        }
    }
}
