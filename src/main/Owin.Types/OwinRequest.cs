using System;
using System.Collections.Concurrent;

namespace Owin.Types
{
    public partial struct OwinRequest
    {
        public static OwinRequest Create()
        {
            var environment = new ConcurrentDictionary<string, object>(StringComparer.Ordinal);
            environment[OwinConstants.RequestHeaders] =
                new ConcurrentDictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            environment[OwinConstants.ResponseHeaders] =
                new ConcurrentDictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            return new OwinRequest(environment);
        }
    }
}
