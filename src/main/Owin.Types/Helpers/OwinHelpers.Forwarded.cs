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
