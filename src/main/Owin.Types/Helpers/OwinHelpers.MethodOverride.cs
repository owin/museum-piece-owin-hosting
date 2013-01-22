using System;
using System.Collections.Generic;

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
