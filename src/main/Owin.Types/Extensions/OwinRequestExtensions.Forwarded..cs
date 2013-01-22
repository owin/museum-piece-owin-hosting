using System;
using Owin.Types.Helpers;

namespace Owin.Types.Extensions
{
    public static partial class OwinRequestExtensions
    {
        public static string GetForwardedScheme(this OwinRequest request)
        {
            return OwinHelpers.GetForwardedScheme(request);
        }

        public static string GetForwardedHost(this OwinRequest request)
        {
            return OwinHelpers.GetForwardedHost(request);
        }

        public static Uri GetForwardedUri(this OwinRequest request)
        {
            return OwinHelpers.GetForwardedUri(request);
        }
        
        public static OwinRequest ApplyForwardedScheme(this OwinRequest request)
        {
            return OwinHelpers.ApplyForwardedScheme(request);
        }
        
        public static OwinRequest ApplyForwardedHost(this OwinRequest request)
        {
            return OwinHelpers.ApplyForwardedHost(request);
        }
        
        public static OwinRequest ApplyForwardedUri(this OwinRequest request)
        {
            return OwinHelpers.ApplyForwardedUri(request);
        }
    }
}
