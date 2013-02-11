// <copyright file="OwinHelpers.MethodOverride.cs" company="Microsoft Open Technologies, Inc.">
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

namespace Owin.Types.Helpers
{
    public static partial class OwinHelpers
    {
        public static string GetMethodOverride(OwinRequest request)
        {
            var method = request.Method;
            if (!string.Equals("POST", method, StringComparison.OrdinalIgnoreCase))
            {
                // override has no effect on POST 
                return method;
            }

            var methodOverride = GetHeader(request.Headers, "X-Http-Method-Override");
            if (string.IsNullOrEmpty(methodOverride))
            {
                return method;
            }

            return methodOverride;
        }

        public static OwinRequest ApplyMethodOverride(OwinRequest request)
        {
            request.Method = GetMethodOverride(request);
            return request;
        }
    }
}
