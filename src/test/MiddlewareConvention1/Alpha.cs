using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MiddlewareConvention1;
using Owin;

namespace Owin
{
    public static partial class Extensions
    {
        public static IAppBuilder UseAlpha(this IAppBuilder builder, string arg1, string arg2)
        {
            return builder.Use(Alpha.Middleware(arg1, arg2));
        }
    }
}

namespace MiddlewareConvention1
{
    public class Alpha
    {
        public static Func<AppDelegate, AppDelegate> Middleware(string arg1, string arg2)
        {
            return app => call =>
                {
                    var thisMiddlewareHandlesThisRequest = false;

                    // may inspect call information here

                    // optionally call information may be modified

                    // middleware may decide to handle request instead 
                    // of passing along

                    if (thisMiddlewareHandlesThisRequest)
                    {
                        // to handle the request, return a task of result 
                        return Execute(call);
                    }

                    // to pass along the request, call the next app
                    return app(call).Then(result =>
                    {
                        // may inspect result information here

                        // optionally result information may be modified, or
                        // a different result may be returned

                        return result;
                    });
                };
        }

        public static Task<ResultParameters> Execute(CallParameters call)
        {
            return TaskHelpers.FromResult(new ResultParameters
            {
                Status = 200,
                Headers = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
                {
                    {"Content-Type", new[] {"text/plain"}}
                },
                Body = output =>
                {
                    using (var writer = new StreamWriter(output))
                    {
                        writer.Write("Welcome to the machine");
                    }
                    return TaskHelpers.Completed();
                },
                Properties = new Dictionary<string, object>(),
            });
        }
    }
}
