using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Owin.Types
{
    public struct OwinResponse
    {
        private readonly IDictionary<string, object> _environment;

        public OwinResponse(IDictionary<string, object> environment)
        {
            _environment = environment;
        }

        public IDictionary<string, object> Environment
        {
            get { return _environment; }
        }

        public T Get<T>(string key)
        {
            object value;
            return _environment.TryGetValue(key, out value) ? (T)value : default(T);
        }

        public OwinResponse Set(string key, object value)
        {
            _environment[key] = value;
            return this;
        }

        public string OwinVersion
        {
            get { return Get<string>(OwinConstants.OwinVersion); }
            set { Set(OwinConstants.OwinVersion, value); }
        }

        public CancellationToken CallCancelled
        {
            get { return Get<CancellationToken>(OwinConstants.CallCancelled); }
            set { Set(OwinConstants.CallCancelled, value); }
        }

        public int StatusCode
        {
            get { return Get<int>(OwinConstants.ResponseStatusCode); }
            set { Set(OwinConstants.ResponseStatusCode, value); }
        }

        public string ReasonPhrase
        {
            get { return Get<string>(OwinConstants.ResponseReasonPhrase); }
            set { Set(OwinConstants.ResponseReasonPhrase, value); }
        }

        public string Protocol
        {
            get { return Get<string>(OwinConstants.ResponseProtocol); }
            set { Set(OwinConstants.ResponseProtocol, value); }
        }

        public IDictionary<string, string[]> Headers
        {
            get { return Get<IDictionary<string, string[]>>(OwinConstants.ResponseHeaders); }
            set { Set(OwinConstants.ResponseHeaders, value); }
        }

        public Stream Body
        {
            get { return Get<Stream>(OwinConstants.ResponseBody); }
            set { Set(OwinConstants.ResponseBody, value); }
        }
    }
}
