using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MiddlewareConvention1;
using MiddlewareConvention2;
using Owin;

namespace StartupConvention1
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

    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseAlpha("a", "b");

            builder.Use(Alpha.Middleware("a", "b"));

            builder.Use(Beta.Middleware("a", "b"));

            builder.UseGamma("a", "b");

            builder.Use(typeof(Gamma), "a", "b");

            builder.Use<AppDelegate>(app => new Gamma(app, "a", "b").Invoke);

            builder.Use(typeof(Delta), "a", "b");

            builder.Use<AppAction>(app => new Delta(app, "a", "b").Invoke);

            builder.Run(this);
        }

        public Task<ResultParameters> Invoke(CallParameters call)
        {
            throw new NotImplementedException();
        }
    }
}