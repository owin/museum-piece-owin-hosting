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
using System.IO;
using System.Threading.Tasks;
using MiddlewareConvention1;
using Utils;

namespace Owin
{
    public static partial class Extensions
    {
        public static IAppBuilder UseGamma(this IAppBuilder builder, string arg1, string arg2)
        {
            return builder.Use(typeof(Gamma), arg1, arg2);
        }
    }
}

namespace MiddlewareConvention1
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Gamma
    {
        readonly AppFunc _app;
        readonly string _arg1;
        readonly string _arg2;

        public Gamma(AppFunc app, string arg1, string arg2)
        {
            _app = app;
            _arg1 = arg1;
            _arg2 = arg2;
        }

        public Task Invoke(IDictionary<string, object> env)
        {
            var helper = new OwinHelper(env);
            if (helper.RequestPath.StartsWith(_arg1, StringComparison.OrdinalIgnoreCase))
            {
                helper.ResponseHeaders["Content-Type"] = new[] { "text/plain" };
                using (var writer = new StreamWriter(helper.OutputStream))
                {
                    writer.Write(_arg2);
                }
                return TaskHelpers.Completed();
            }

            // to pass along the request, call the next app
            return _app(env);
        }
    }
}
