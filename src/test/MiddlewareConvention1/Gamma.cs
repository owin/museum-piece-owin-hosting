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
        public static IAppBuilder UseGamma(this IAppBuilder builder, string arg1, string arg2)
        {
            return builder.Use(typeof(Gamma), arg1, arg2);
        }
    }
}

namespace MiddlewareConvention1
{
    public class Gamma
    {
        readonly AppDelegate _app;
        readonly string _arg1;
        readonly string _arg2;

        public Gamma(AppDelegate app, string arg1, string arg2)
        {
            _app = app;
            _arg1 = arg1;
            _arg2 = arg2;
        }

        public Task<ResultParameters> Invoke(CallParameters call)
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
            return _app(call).Then(result =>
            {
                // may inspect result information here

                // optionally result information may be modified, or
                // a different result may be returned

                return result;
            });
        }

        public Task<ResultParameters> Execute(CallParameters call)
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
