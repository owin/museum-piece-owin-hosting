using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Owin.Types
{
    public struct OwinEnvironment
    {
        private readonly IDictionary<string, object> _environment;

        public OwinEnvironment(IDictionary<string, object> environment)
        {
            _environment = environment;
        }

        public static OwinEnvironment Create()
        {
            var environment = new ConcurrentDictionary<string, object>(StringComparer.Ordinal);
            environment[OwinConstants.RequestHeaders] =
                new ConcurrentDictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            environment[OwinConstants.ResponseHeaders] =
                new ConcurrentDictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            return new OwinEnvironment(environment);
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

        public OwinEnvironment Set(string key, object value)
        {
            _environment[key] = value;
            return this;
        }

        public OwinRequest Request
        {
            get { return new OwinRequest(_environment); }
        }

        public OwinResponse Response
        {
            get { return new OwinResponse(_environment); }
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
    }
}