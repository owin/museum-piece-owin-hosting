using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Owin.Builder.Utils;
using Shouldly;
using Xunit;

namespace Owin.Builder.Tests
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

    public class ConversionsTests
    {
        [Fact]
        public void AppDelegateShouldBeOwinDelegate()
        {
            Conversions.IsOwinDelegate(typeof(AppDelegate)).ShouldBe(true);
        }

        [Fact]
        public void FuncOfStructsShouldBeOwinDelegate()
        {
            Conversions.IsOwinDelegate(typeof(Func<CallParameters, Task<ResultParameters>>)).ShouldBe(true);
        }

        public delegate Task<ResultParameters> AnotherNamedDelegate(CallParameters call);

        [Fact]
        public void AnotherNamedDelegateShouldBeOwinDelegate()
        {
            Conversions.IsOwinDelegate(typeof(AnotherNamedDelegate)).ShouldBe(true);
        }

        public struct OtherCallParameters
        {
            public Stream Body;
            public IDictionary<string, object> Environment;
            public IDictionary<string, string[]> Headers;
        }

        public struct OtherResultParameters
        {
            public int Status;
            public IDictionary<string, string[]> Headers;
            public Func<Stream, Task> Body;
            public IDictionary<string, object> Properties;
        }

        [Fact]
        public void FuncOfOtherStructsShouldBeOwinDelegate()
        {
            Conversions.IsOwinDelegate(typeof(Func<OtherCallParameters, Task<OtherResultParameters>>)).ShouldBe(true);
        }

        public delegate Task<OtherResultParameters> AnotherNamedDelegateOfOtherStructs(OtherCallParameters call);

        [Fact]
        public void AnotherNamedDelegateOfOtherStructsShouldBeOwinDelegate()
        {
            Conversions.IsOwinDelegate(typeof(AnotherNamedDelegate)).ShouldBe(true);
        }

        [Fact]
        public void AppActionShouldBeOwinDelegate()
        {
            Conversions.IsOwinDelegate(typeof(AppAction)).ShouldBe(true);
        }

        public delegate Task<Tuple<
                IDictionary<string, object>,
                int,
                IDictionary<string, string[]>,
                Func<Stream, Task>>>
            NamedDelegateWithoutStructs(
                IDictionary<string, object> env,
                IDictionary<string, string[]> headers,
                Stream input);

        [Fact]
        public void NamedDelegateWithoutStructsShouldBeOwinDelegate()
        {
            Conversions.IsOwinDelegate(typeof(NamedDelegateWithoutStructs)).ShouldBe(true);
        }


        [Fact]
        public Task CallToAppDelegateFromAppActionShouldBeCreatable()
        {
            var convert = Conversions.EmitConversion(typeof(AppDelegate), typeof(AppAction));

            var theCall = default(CallParameters);
            var theResult = new ResultParameters { Status = 655321 };
            AppDelegate given = call =>
            {
                theCall = call;
                return TaskHelpers.FromResult(theResult);
            };

            var needed = (AppAction)convert(given);

            var env = new Dictionary<string, object>();
            var headers = new Dictionary<string, string[]>();
            var input = Stream.Null;

            return needed(env, headers, input)
                .Then(result =>
                {
                    result.Item2.ShouldBe(655321);
                    theCall.Environment.ShouldBeSameAs(env);
                });
        }

        void Prototype()
        {
            //Expression<Func<AppAction, AppDelegate>> conversion =
            //    app => call =>
            //        app(call.Environment,
            //            call.Headers,
            //            call.Body).Then(result =>
            //                new ResultParameters
            //                {
            //                    Properties = result.Item1,
            //                    Status = result.Item2,
            //                    Headers = result.Item3,
            //                    Body = result.Item4,
            //                },
            //                default(CancellationToken),
            //                false);
        }

        [Fact]
        public Task CallToAppActionFromAppDelegateShouldBeCreatable()
        {


            var convert = Conversions.EmitConversion(typeof(AppAction), typeof(AppDelegate));

            var theEnv = default(IDictionary<string, object>);
            var theResult = TaskHelpers.FromResult(new ResultTuple(
                new Dictionary<string, object>(),
                655321,
                new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase),
                output => TaskHelpers.Completed()));

            AppAction given = (env, headers, body) =>
            {
                theEnv = env;
                return theResult;
            };

            var needed = (AppDelegate)convert(given);

            var call = new CallParameters
            {
                Environment = new Dictionary<string, object>(),
                Headers = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase),
                Body = Stream.Null,
            };

            return needed(call)
                .Then(result =>
                {
                    result.Status.ShouldBe(655321);
                    theEnv.ShouldBeSameAs(call.Environment);
                });
        }
    }
}
