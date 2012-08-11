using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin.Builder.Tests.Middlewares
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


    public static partial class StandardBehavior
    {
        public static Task<ResultTuple> Execute(
            IDictionary<string, object> env,
            IDictionary<string, string[]> headers,
            Stream body,
            AppAction app,
            string arg1,
            string arg2)
        {
            AddValue(env, "arg1", arg1);
            return app.Invoke(env, headers, body)
                .Then(result =>
                {
                    AddValue(env, "arg2", arg2);
                    AddValue(result.Item1, "arg2", arg2);
                    return result;
                });
        }
    }

    public class AlphaAppAction
    {
        public AppAction Invoke(AppAction app, string arg1, string arg2)
        {
            return (env, headers, body) => StandardBehavior.Execute(env, headers, body, app, arg1, arg2);
        }
    }

    public class BetaAppAction
    {
        readonly AppAction _app;
        readonly string _arg1;
        readonly string _arg2;

        public BetaAppAction(AppAction app, string arg1, string arg2)
        {
            _app = app;
            _arg1 = arg1;
            _arg2 = arg2;
        }

        public Task<ResultTuple> Invoke(
            IDictionary<string, object> env,
            IDictionary<string, string[]> headers,
            Stream body)
        {
            return StandardBehavior.Execute(env, headers, body, _app, _arg1, _arg2);
        }
    }

    public class GammaAppAction
    {
        public BetaAppAction Middleware(AppAction app, string arg1, string arg2)
        {
            return new BetaAppAction(app, arg1, arg2);
        }
    }
}
