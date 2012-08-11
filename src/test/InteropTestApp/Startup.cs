using System;
using System.Threading.Tasks;
using MiddlewareConvention1;
using MiddlewareConvention2;
using Owin;

namespace InteropTestApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseAlpha("one", "two");

            builder.Use(Beta.Middleware("three", "four"));

            builder.Use((AppDelegate)Main);

            builder.Properties["hello"] = "world";

            builder.AddSignatureConversion(new Func<CustomThing, AppDelegate>(Convert1));
            builder.AddSignatureConversion(new Func<AppDelegate, CustomThing>(Convert2));

            var thing = builder.BuildNew<CustomThing>(
                x => x.UseAlpha("five", "six"));
        }

        public Task<ResultParameters> Main(CallParameters call)
        {
            throw new NotImplementedException();
        }

        public AppDelegate Convert1(CustomThing thing)
        {
            return call =>
            {
                thing.Invoke("called");
                return null;
            };
        }

        public CustomThing Convert2(AppDelegate app)
        {
            var thing = new CustomThing();
            return thing;
        }

        public class CustomThing
        {
            public string Invoke(string request)
            {
                return "response";
            }
        }
    }
}
