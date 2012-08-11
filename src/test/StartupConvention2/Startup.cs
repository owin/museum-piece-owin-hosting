using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MiddlewareConvention2;

namespace StartupConvention2
{
    using AppAction = Func< // Call
       IDictionary<string, object>, // Environment
       IDictionary<string, string[]>, // Headers
       Stream, // Body
       Task<Tuple< // Result
           IDictionary<string, object>, // Properties
           int, // Status
           IDictionary<string, string[]>, // Headers
           Func< // CopyTo
               Stream, // Body
               Task>>>>; // Done

    using ResultTuple = Tuple< //Result
        IDictionary<string, object>, // Properties
        int, // Status
        IDictionary<string, string[]>, // Headers
        Func< // CopyTo
            Stream, // Body
            Task>>; // Done

    public class Startup
    {
        public AppAction Configuration(IDictionary<string, object> properties)
        {
            AppAction app = Main;

            app = Beta.Middleware("Three", "Four").Invoke(app);

            return app;
        }

        public Task<ResultTuple> Main(
            IDictionary<string, object> env,
            IDictionary<string, string[]> headers,
            Stream input)
        {
            throw new NotImplementedException();
        }
    }
}