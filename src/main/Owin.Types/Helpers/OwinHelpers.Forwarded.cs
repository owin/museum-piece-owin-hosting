// <copyright file="OwinHelpers.Forwarded.cs" company="Microsoft Open Technologies, Inc.">
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
using System.Linq;

namespace Owin.Types.Helpers
{
    public static partial class OwinHelpers
    {
        public static string GetForwardedScheme(OwinRequest request)
        {
            var headers = request.Headers;

            var forwardedSsl = GetHeader(headers, "X-Forwarded-Ssl");
            if (forwardedSsl != null && string.Equals(forwardedSsl, "on", StringComparison.OrdinalIgnoreCase))
            {
                return "https";
            }

            var forwardedScheme = GetHeader(headers, "X-Forwarded-Scheme");
            if (!string.IsNullOrWhiteSpace(forwardedScheme))
            {
                return forwardedScheme;
            }

            var forwardedProto = GetHeaderSplit(headers, "X-Forwarded-Proto");
            if (forwardedProto != null)
            {
                forwardedScheme = forwardedProto.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(forwardedScheme))
                {
                    return forwardedScheme;
                }
            }

            return request.Scheme;
        }

        public static string GetForwardedHost(OwinRequest request)
        {
            var headers = request.Headers;

            var forwardedHost = GetHeaderSplit(headers, "X-Forwarded-Host");
            if (forwardedHost != null)
            {
                return forwardedHost.Last();
            }

            var host = GetHeader(headers, "Host");
            if (!string.IsNullOrWhiteSpace(host))
            {
                return host;
            }

            var localIpAddress = request.LocalIpAddress ?? "localhost";
            var localPort = request.LocalPort;
            return string.IsNullOrWhiteSpace(localPort) ? localIpAddress : (localIpAddress + ":" + localPort);
        }

        public static Uri GetForwardedUri(OwinRequest request)
        {
            var queryString = request.QueryString;

            return string.IsNullOrWhiteSpace(queryString) 
                ? new Uri(GetForwardedScheme(request) + "://" + GetForwardedHost(request) + request.PathBase + request.Path) 
                : new Uri(GetForwardedScheme(request) + "://" + GetForwardedHost(request) + request.PathBase + request.Path + "?" + queryString);
        }

        public static OwinRequest ApplyForwardedScheme(OwinRequest request)
        {
            request.Scheme = GetForwardedScheme(request);
            return request;
        }

        public static OwinRequest ApplyForwardedHost(OwinRequest request)
        {
            request.Host = GetForwardedHost(request);
            return request;
        }

        public static OwinRequest ApplyForwardedUri(OwinRequest request)
        {
            return ApplyForwardedHost(ApplyForwardedScheme(request));
        }

    }
}
