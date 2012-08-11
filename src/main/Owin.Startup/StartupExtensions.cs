using System;

namespace Owin
{
    public static class StartupExtensions
    {
        public static IAppBuilder UseType<TMiddleware>(this IAppBuilder builder, params object[] args)
        {
            return builder.Use(typeof(TMiddleware), args);
        }

        public static IAppBuilder UseFunc<TApp>(this IAppBuilder builder, Func<TApp, TApp> middleware)
        {
            return builder.Use(middleware);
        }

        public static void Run(this IAppBuilder builder, object app)
        {
            builder.Use(new Func<object, object>(ignored => app));
        }

        public static TApp Build<TApp>(this IAppBuilder builder)
        {
            return (TApp)builder.Build(typeof(TApp));
        }

        public static TApp BuildNew<TApp>(
           this IAppBuilder builder,
           Action<IAppBuilder> configuration)
        {
            var nested = builder.New();
            configuration(nested);
            return nested.Build<TApp>();
        }
    }
}
