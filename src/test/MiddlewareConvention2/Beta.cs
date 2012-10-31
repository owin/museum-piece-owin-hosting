// Licensed under one or more contributor license agreements.  
// See the NOTICE.txt file distributed with this work for 
// additional information regarding copyright ownership.  The 
// copyright owners license this file to you under the Apache 
// License, Version 2.0 (the "License"); you may not use this 
// file except in compliance with the License.  You may obtain 
// a copy of the License at
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
using Utils;

namespace MiddlewareConvention2
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Beta
    {
        public static Func<AppFunc, AppFunc> Middleware(string arg1, string arg2)
        {
            return app => env =>
            {
                var helper = new OwinHelper(env);
                if (helper.RequestPath.StartsWith(arg1, StringComparison.OrdinalIgnoreCase))
                {
                    helper.ResponseHeaders["Content-Type"] = new[] { "text/plain" };
                    using (var writer = new StreamWriter(helper.OutputStream))
                    {
                        writer.Write(arg2);
                    }
                    return TaskHelpers.Completed();
                }

                // to pass along the request, call the next app
                return app(env);
            };
        }
    }
}
