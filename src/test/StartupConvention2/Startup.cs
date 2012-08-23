using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MiddlewareConvention1;
using MiddlewareConvention2;

namespace StartupConvention2
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    using ResultTuple = Tuple< //Result
        IDictionary<string, object>, // Properties
        int, // Status
        IDictionary<string, string[]>, // Headers
        Func< // CopyTo
            Stream, // Body
            Task>>; // Done

    public class Startup
    {
        public AppFunc Configuration(IDictionary<string, object> properties)
        {
            AppFunc app = Main;

            app = Alpha.Middleware(app, "One", "Two");

            app = Beta.Middleware("Three", "Four").Invoke(app);

            app = new Gamma(app, "Five", "Six").Invoke;

            app = new Delta(app, "Seven", "Eight").Invoke;

            return app;
        }

        public Task Main(IDictionary<string, object> env)
        {
            throw new NotImplementedException();
        }
    }
}