using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MiddlewareConvention1;
using MiddlewareConvention2;
using Owin;

namespace StartupConvention1
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseAlpha("a", "b");

            builder.UseFunc(app => Alpha.Middleware(app, "a", "b"));

            builder.UseFunc(Alpha.Middleware, "a", "b");

            builder.Use(Beta.Middleware("a", "b"));

            builder.UseFunc(Beta.Middleware, "a", "b");

            builder.UseGamma("a", "b");

            builder.Use(typeof(Gamma), "a", "b");

            builder.UseType<Gamma>("a", "b");

            builder.UseFunc<AppFunc>(app => new Gamma(app, "a", "b").Invoke);

            builder.Use(typeof(Delta), "a", "b");

            builder.UseType<Delta>("a", "b");

            builder.UseFunc<AppFunc>(app => new Delta(app, "a", "b").Invoke);

            builder.Run(this);
        }

        public Task Invoke(IDictionary<string, object> env)
        {
            throw new NotImplementedException();
        }
    }
}