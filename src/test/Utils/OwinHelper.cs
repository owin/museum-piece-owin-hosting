using System;
using System.Collections.Generic;
using System.IO;

namespace Utils
{
    public class OwinHelper
    {
        public IDictionary<string, object> Env { get; private set; }

        public OwinHelper(IDictionary<string, object> env)
        {
            Env = env;
        }

        public OwinHelper()
        {
            Env = new Dictionary<string, object>
            {
                {"owin.RequestHeaders",new Dictionary<string,string[]>(StringComparer.OrdinalIgnoreCase)},
                {"owin.ResponseHeaders",new Dictionary<string,string[]>(StringComparer.OrdinalIgnoreCase)},
            };
        }

        public string RequestPath
        {
            get { return Get<string>("owin.RequestPath"); }
        }

        public IDictionary<string, string[]> ResponseHeaders
        {
            get { return Get<IDictionary<string, string[]>>("owin.ResponseHeaders"); }
        }

        public Stream OutputStream
        {
            get { return Get<Stream>("owin.ResponseBody"); }
        }

        public int ResponseStatusCode
        {
            get { return Get<int>("owin.ResponseStatusCode"); }
        }

        T Get<T>(string key)
        {
            object value;
            return Env.TryGetValue(key, out value) ? (T)value : default(T);
        }
    }
}
