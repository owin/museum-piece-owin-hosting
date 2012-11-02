// Licensed to Monkey Square, Inc. under one or more contributor 
// license agreements.  See the NOTICE file distributed with 
// this work or additional information regarding copyright 
// ownership.  Monkey Square, Inc. licenses this file to you 
// under the Apache License, Version 2.0 (the "License"); you 
// may not use this file except in compliance with the License.
// You may obtain a copy of the License at 
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiddlewareConvention1;
using MiddlewareConvention2;
using Owin;

namespace StartupConvention1
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseAlpha("a", "b");

            builder.UseFunc(app => Alpha.Middleware(app, "a", "b"));

            builder.UseFunc(Alpha.Middleware, "a", "b");

            builder.Use(Beta.Middleware("a", "b"));

            builder.UseFunc(Beta.Middleware, "a", "b");

            builder.UseGamma("a", "b");

            builder.Use(typeof(Gamma), "a", "b");

            builder.UseType<Gamma>("a", "b");

            builder.UseFunc<AppFunc>(app => new Gamma(app, "a", "b").Invoke);

            builder.Use(typeof(Delta), "a", "b");

            builder.UseType<Delta>("a", "b");

            builder.UseFunc<AppFunc>(app => new Delta(app, "a", "b").Invoke);

            builder.Run(this);
        }

        public Task Invoke(IDictionary<string, object> env)
        {
            throw new NotImplementedException();
        }
    }
}