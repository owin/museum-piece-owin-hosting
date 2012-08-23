using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Utils;

namespace MiddlewareConvention2
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Beta
    {
        public static Func<AppFunc, AppFunc> Middleware(string arg1, string arg2)
        {
            return app => env =>
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
