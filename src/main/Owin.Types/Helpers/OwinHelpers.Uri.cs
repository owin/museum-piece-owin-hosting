// <copyright file="OwinHelpers.Uri.cs" company="Microsoft Open Technologies, Inc.">
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
        public static string GetHost(OwinRequest request)
        {
            var headers = request.Headers;

            var host = GetHeader(headers, "Host");
            if (!string.IsNullOrWhiteSpace(host))
            {
                return host;
            }

            var localIpAddress = request.LocalIpAddress ?? "localhost";
            var localPort = request.LocalPort;
            return string.IsNullOrWhiteSpace(localPort) ? localIpAddress : (localIpAddress + ":" + localPort);
        }

        public static Uri GetUri(OwinRequest request)
        {
            var queryString = request.QueryString;

            return string.IsNullOrWhiteSpace(queryString)
                ? new Uri(request.Scheme + "://" + GetHost(request) + request.PathBase + request.Path)
                : new Uri(request.Scheme + "://" + GetHost(request) + request.PathBase + request.Path + "?" + queryString);
        }
    }
}
