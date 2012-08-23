using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Utils;

namespace MiddlewareConvention2
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Delta
    {
        readonly AppFunc _app;
        readonly string _arg1;
        readonly string _arg2;

        public Delta(AppFunc app, string arg1, string arg2)
        {
            _app = app;
            _arg1 = arg1;
            _arg2 = arg2;
        }

        public Task Invoke(IDictionary<string, object> env)
        {
            var helper = new OwinHelper(env);
            if (helper.RequestPath.StartsWith(_arg1, StringComparison.OrdinalIgnoreCase))
            {
                helper.ResponseHeaders["Content-Type"] = new[] { "text/plain" };
                using (var writer = new StreamWriter(helper.OutputStream))
                {
                    writer.Write(_arg2);
                }
                return TaskHelpers.Completed();
            }

            // to pass along the request, call the next app
            return _app(env);
        }
    }
}
