using System;
using System.Collections.Concurrent;

namespace Owin.Types
{
    public partial struct OwinOpaqueParameters
    {
        public static OwinOpaqueParameters Create()
        {
            return new OwinOpaqueParameters(new ConcurrentDictionary<string, object>(StringComparer.Ordinal));
        }
    }
}
