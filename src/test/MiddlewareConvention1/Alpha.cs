using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MiddlewareConvention1;
using Utils;

namespace Owin
{
    public static partial class Extensions
    {
        public static IAppBuilder UseAlpha(this IAppBuilder builder, string arg1, string arg2)
        {
            return builder.UseFunc(Alpha.Middleware, arg1, arg2);
        }
    }
}

namespace MiddlewareConvention1
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Alpha
    {
        public static AppFunc Middleware(AppFunc app, string arg1, string arg2)
        {
            return env =>
            {
                var helper = new OwinHelper(env);
                if (helper.RequestPath.StartsWith(arg1, StringComparison.OrdinalIgnoreCase))
                {
                    helper.ResponseHeaders["Content-Type"] = new[] { "text/plain" };
                    using (var writer = new StreamWriter(helper.OutputStream))
                    {
                        writer.Write(arg2);
                    }
                    return TaskHelpers.Completed();
                }

                // to pass along the request, call the next app
                return app(env);
            };
        }
    }
}
