using System;
using System.Collections.Concurrent;

namespace Owin.Types
{
    public partial struct OwinOpaqueParameters
    {
        public static OwinWebSocketParameters Create()
        {
            return new OwinWebSocketParameters(new ConcurrentDictionary<string, object>(StringComparer.Ordinal));
        }
    }
}
