using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Owin.Types
{
    public struct OwinRequest
    {
        private readonly IDictionary<string, object> _environment;

        public OwinRequest(IDictionary<string, object> environment)
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

        public OwinRequest Set(string key, object value)
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

        public string Scheme
        {
            get { return Get<string>(OwinConstants.RequestScheme); }
            set { Set(OwinConstants.RequestScheme, value); }
        }

        public string Method
        {
            get { return Get<string>(OwinConstants.RequestMethod); }
            set { Set(OwinConstants.RequestMethod, value); }
        }

        public string PathBase
        {
            get { return Get<string>(OwinConstants.RequestPathBase); }
            set { Set(OwinConstants.RequestPathBase, value); }
        }

        public string Path
        {
            get { return Get<string>(OwinConstants.RequestPath); }
            set { Set(OwinConstants.RequestPath, value); }
        }

        public string QueryString
        {
            get { return Get<string>(OwinConstants.RequestQueryString); }
            set { Set(OwinConstants.RequestQueryString, value); }
        }

        public string Protocol
        {
            get { return Get<string>(OwinConstants.RequestProtocol); }
            set { Set(OwinConstants.RequestProtocol, value); }
        }

        public IDictionary<string, string[]> Headers
        {
            get { return Get<IDictionary<string, string[]>>(OwinConstants.RequestHeaders); }
            set { Set(OwinConstants.RequestHeaders, value); }
        }

        public Stream Body
        {
            get { return Get<Stream>(OwinConstants.RequestBody); }
            set { Set(OwinConstants.RequestBody, value); }
        }
    }
}
