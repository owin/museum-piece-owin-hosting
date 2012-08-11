using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MiddlewareConvention2
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

    public class Delta
    {
        readonly AppAction _app;
        readonly string _arg1;
        readonly string _arg2;

        public Delta(AppAction app, string arg1, string arg2)
        {
            _app = app;
            _arg1 = arg1;
            _arg2 = arg2;
        }

        public Task<ResultTuple> Invoke(
            IDictionary<string, object> env,
            IDictionary<string, string[]> headers,
            Stream input)
        {
            var thisMiddlewareHandlesThisRequest = false;

            // may inspect call information here

            // optionally call information may be modified

            // middleware may decide to handle request instead 
            // of passing along

            if (thisMiddlewareHandlesThisRequest)
            {
                // to handle the request, return a task of result 
                return Execute(env, headers, input);
            }

            // to pass along the request, call the next app
            return _app(env, headers, input).Then(result =>
            {
                // may inspect result information here

                // optionally result information may be modified, or
                // a different result may be returned

                return result;
            });
        }

        public Task<ResultTuple> Execute(
            IDictionary<string, object> env,
            IDictionary<string, string[]> headers,
            Stream input)
        {
            return TaskHelpers.FromResult(new ResultTuple(
                new Dictionary<string, object>(),
                200,
                new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
                    {
                        {"Content-Type", new[] {"text/plain"}}
                    },
                output =>
                {
                    using (var writer = new StreamWriter(output))
                    {
                        writer.Write("Welcome to the machine");
                    }
                    return TaskHelpers.Completed();
                }));

        }
    }
}
