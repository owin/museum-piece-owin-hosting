using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Owin
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    using System.Diagnostics.CodeAnalysis;

    public static partial class StartupExtensions
    {
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "By design")]
        public static IAppBuilder UseType<TMiddleware>(this IAppBuilder builder, params object[] args)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.Use(typeof(TMiddleware), args);
        }

        public static IAppBuilder UseType(this IAppBuilder builder, Type type, params object[] args)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.Use(type, args);
        }
    }
}
