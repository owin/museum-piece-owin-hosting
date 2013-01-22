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
