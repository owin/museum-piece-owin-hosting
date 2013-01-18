using System.Collections.Generic;

namespace Owin.Types
{

    public partial struct OwinRequest
    {
        private readonly IDictionary<string, object> _dictionary;

        public OwinRequest(IDictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public IDictionary<string, object> Dictionary
        {
            get { return _dictionary; }
        }

        public T Get<T>(string key)
        {
            object value;
            return _dictionary.TryGetValue(key, out value) ? (T)value : default(T);
        }

        public OwinRequest Set(string key, object value)
        {
            _dictionary[key] = value;
            return this;
        }
    }

    public partial struct OwinResponse
    {
        private readonly IDictionary<string, object> _dictionary;

        public OwinResponse(IDictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public IDictionary<string, object> Dictionary
        {
            get { return _dictionary; }
        }

        public T Get<T>(string key)
        {
            object value;
            return _dictionary.TryGetValue(key, out value) ? (T)value : default(T);
        }

        public OwinResponse Set(string key, object value)
        {
            _dictionary[key] = value;
            return this;
        }
    }

    public partial struct OwinWebSocket
    {
        private readonly IDictionary<string, object> _dictionary;

        public OwinWebSocket(IDictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public IDictionary<string, object> Dictionary
        {
            get { return _dictionary; }
        }

        public T Get<T>(string key)
        {
            object value;
            return _dictionary.TryGetValue(key, out value) ? (T)value : default(T);
        }

        public OwinWebSocket Set(string key, object value)
        {
            _dictionary[key] = value;
            return this;
        }
    }
}
