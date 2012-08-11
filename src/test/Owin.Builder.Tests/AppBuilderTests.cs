using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Owin.Builder.Tests.Middlewares;
using Shouldly;
using Xunit;

namespace Owin.Builder.Tests
{

    public class AppBuilderTests
    {
        [Fact]
        public void DelegateShouldBeCalledToAddMiddlewareAroundTheDefaultApp()
        {
            var theNext = "next";
            var theMiddle = "middle";
            var theDefault = "default";

            Func<string, string> middleware = next =>
            {
                theNext = next;
                return theMiddle;
            };

            var builder = new AppBuilder();
            builder.Properties["builder.DefaultApp"] = theDefault;
            var theApp = builder.BuildNew<string>(x => x.Use(middleware));

            builder.Run(theApp);

            theNext.ShouldBeSameAs(theDefault);
            theApp.ShouldBeSameAs(theMiddle);
            theApp.ShouldNotBeSameAs(theDefault);
        }

        [Fact]
        public void ConversionShouldBeCalledBetweenDifferentSignatures()
        {
            object theDefault = "42";

            Func<string, int> convert1 = app => int.Parse(app, CultureInfo.InvariantCulture) + 1;
            Func<int, string> convert2 = app => app.ToString(CultureInfo.InvariantCulture) + "2";

            Func<string, string> middleware1 = app => app + "3";
            Func<int, int> middleware2 = app => app + 4;

            var builder = new AppBuilder();
            builder.AddSignatureConversion(convert1);
            builder.AddSignatureConversion(convert2);
            builder.Properties["builder.DefaultApp"] = theDefault;

            var theApp = builder.BuildNew<int>(x => x.Use(middleware1).Use(middleware2));

            // "42" + 1: 43         // theDefault passed through convert1 for next middleware
            // 43 + 4: 47           // passed through middleware2
            // 47 + "2": "472"      // passed through convert2 for next middleware
            // "472" + "3": "4723"  // passed through middleware1
            // "4723" + 1: 4724     // passed through convert1 to return

            theApp.ShouldBe(4724);
        }

        [Fact]
        public void InstanceMemberNamedInvokeShouldQualifyAsMiddlewareFactory()
        {
            Func<int, string> theDefault = call => "Hello[" + call + "]";

            var builder = new AppBuilder();
            builder.Properties["builder.DefaultApp"] = theDefault;

            var theApp = builder.BuildNew<Func<int, string>>(
                x => x
                    .Use(new StringPlusValue(" world!"))
                    .Use(new StringPlusValue(" there,")));

            theApp(42).ShouldBe("Hello[42] there, world!");
        }

        class StringPlusValue
        {
            readonly string _value;

            public StringPlusValue(string value)
            {
                _value = value;
            }

            public Func<int, string> Invoke(Func<int, string> app)
            {
                return call => app(call) + _value;
            }
        }

        [Fact]
        public void DelegateShouldQualifyAsAppWithRun()
        {
            Func<int, string> theDefault = call => "Hello[" + call + "]";
            Func<int, string> theSite = call => "Called[" + call + "]";

            var builder = new AppBuilder();
            builder.Properties["builder.DefaultApp"] = theDefault;

            var theApp = builder.BuildNew<Func<int, string>>(x => x.Run(theSite));

            theApp(42).ShouldBe("Called[42]");
        }

        [Fact]
        public void InstanceMemberNamedInvokeShouldQualifyAsAppWithRun()
        {
            var theSite = new MySite();

            var builder = new AppBuilder();

            var theApp = builder.BuildNew<Func<int, string>>(x => x.Run(theSite));

            theApp(42).ShouldBe("Called[42]");
        }

        public class MySite
        {
            public string Invoke(int call)
            {
                return "Called[" + call + "]";
            }
        }

        [Fact]
        public void TypeofClassConstructorsShouldQualifyAsMiddlewareFactory()
        {
            Func<int, string> theDefault = call => "Hello[" + call + "]";

            var builder = new AppBuilder();
            builder.Properties["builder.DefaultApp"] = theDefault;

            var theApp = builder.BuildNew<Func<int, string>>(
                x => x
                    .Use(typeof(StringPlusValue2), " world!")
                    .Use(typeof(StringPlusValue2), " there,"));

            theApp(42).ShouldBe("Hello[42] there, world!");
        }


        class StringPlusValue2
        {
            readonly Func<int, string> _app;
            readonly string _value;

            public StringPlusValue2(Func<int, string> app)
            {
                _app = app;
                _value = " PlusPlus";
            }

            public StringPlusValue2(Func<int, string> app, string value)
            {
                _app = app;
                _value = value;
            }

            public string Invoke(int call)
            {
                return _app(call) + _value;
            }
        }

        [Fact]
        public Task ChainingMiddlewareShouldConvertAsNeeded()
        {
            AppDelegate theDefault = _ => TaskHelpers.FromResult(new ResultParameters { Properties = new Dictionary<string, object>() });

            var builder = new AppBuilder();
            builder.Properties["builder.DefaultApp"] = theDefault;
            builder.Use(new AlphaAppDelegate(), "1", "a");
            builder.Use(new AlphaAppAction(), "2", "b");
            builder.Use(typeof(BetaAppDelegate), "3", "c");
            builder.Use(typeof(BetaAppDelegate), "", "");
            builder.Use(typeof(BetaAppAction), "4", "d");
            builder.UseType<BetaAppDelegate>("5", "e");
            builder.UseType<BetaAppAction>("6", "f");
            builder.Use(new GammaAppDelegate(), "7", "g");
            builder.Use(new GammaAppAction(), "8", "h");

            var app = builder.Build<AppDelegate>();
            var call = new CallParameters { Environment = new Dictionary<string, object>() };
            return app(call)
                .Then(result =>
                {
                    call.Environment["arg1"].ShouldBe("12345678");
                    call.Environment["arg2"].ShouldBe("hgfedcba");
                    result.Properties["arg2"].ShouldBe("hgfedcba");
                });
        }

        public delegate string AppOne(string call);
        public delegate string AppTwo(string call);

        [Fact]
        public void DelegatesWithIdenticalParametersShouldConvertAutomatically()
        {
            var builder = new AppBuilder();
            builder.Properties["builder.DefaultApp"] = new Func<string, string>(call => call);
            builder.UseFunc<AppOne>(next => call => next(call) + "1");
            builder.UseFunc<AppTwo>(next => call => next(call) + "2");
            builder.UseFunc<Func<string, string>>(next => call => next(call) + "3");
            var app = builder.Build<AppTwo>();
            app("0").ShouldBe("0321");
        }

        [Fact]
        public Task TheDefaultDefaultShouldBe404()
        {
            var builder = new AppBuilder();
            var app = builder.Build<AppDelegate>();

            return app(new CallParameters()).Then(result => result.Status.ShouldBe(404));
        }
    }
}

