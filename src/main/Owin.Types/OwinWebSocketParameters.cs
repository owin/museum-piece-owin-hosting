using System;
using System.Collections.Concurrent;

namespace Owin.Types
{
    public partial struct OwinWebSocketParameters
    {
        public static OwinWebSocketParameters Create()
        {
            return new OwinWebSocketParameters(new ConcurrentDictionary<string, object>(StringComparer.Ordinal));
        }

        public static OwinWebSocketParameters Create(string subProtocol)
        {
            return new OwinWebSocketParameters(new ConcurrentDictionary<string, object>(StringComparer.Ordinal))
            {
                SubProtocol = subProtocol
            };
        }
    }
}
