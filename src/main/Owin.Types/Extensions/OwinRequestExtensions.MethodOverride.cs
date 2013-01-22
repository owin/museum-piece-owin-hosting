using Owin.Types.Helpers;

namespace Owin.Types.Extensions
{
    public static partial class OwinRequestExtensions
    {
        public static string GetMethodOverride(this OwinRequest request)
        {
            return OwinHelpers.GetMethodOverride(request);
        }

        public static OwinRequest ApplyMethodOverride(this OwinRequest request)
        {
            return OwinHelpers.ApplyMethodOverride(request);
        }
    }
}
