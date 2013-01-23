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
using MiddlewareConvention3;
using Utils;

namespace Owin
{
    public static partial class Extensions
    {
        public static IAppBuilder UseTheta(this IAppBuilder builder, Theta instance, string arg1, string arg2)
        {
            return builder.Use(instance, arg1, arg2);
        }
    }
}

namespace MiddlewareConvention3
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Theta
    {
        private AppFunc _next;
        private string _arg1, _arg2;

        public Theta(object myConstructorParameter)
        {
            if (myConstructorParameter == null)
            {
                throw new ArgumentNullException("myConstructorParameter");
            }
        }

        public void Initialize(AppFunc next, string arg1, string arg2)
        {
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }
            if (_next != null)
            {
                throw new InvalidOperationException(); // Called more than once
            }

            _next = next;
            _arg1 = arg1;
            _arg2 = arg2;
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            if (_next == null)
            {
                // Or 404?
                throw new InvalidOperationException(); // initialize not called
            }

            var helper = new OwinHelper(environment);
            if (helper.RequestPath.StartsWith(_arg1, StringComparison.OrdinalIgnoreCase))
            {
                helper.ResponseHeaders["Content-Type"] = new[] { "text/plain" };
                using (var writer = new StreamWriter(helper.OutputStream))
                {
                    writer.Write(_arg2);
                }
                return TaskHelpers.Completed();
            }

            return _next(environment);
        }
    }
}
