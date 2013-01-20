using System;
using System.Linq;
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


        public string GetHeader(string key)
        {
            string[] values = GetHeaderUnmodified(key);
            return values == null ? null : string.Join(",", values);
        }

        public IEnumerable<string> GetHeaderSplit(string key)
        {
            string[] values = GetHeaderUnmodified(key);
            return values == null ? null : values.SelectMany(SplitHeader);
        }

        public string[] GetHeaderUnmodified(string key)
        {
            string[] values;
            return Headers.TryGetValue(key, out values) ? values : null;
        }

        private static readonly Func<string, string[]> SplitHeader = header => header.Split(new[] { ',' });

        public OwinRequest SetHeader(string key, string value)
        {
            Headers[key] = new[] { value };
            return this;
        }

        public OwinRequest SetHeaderJoined(string key, params string[] values)
        {
            Headers[key] = new[] { string.Join(",", values) };
            return this;
        }

        public OwinRequest SetHeaderJoined(string key, IEnumerable<string> values)
        {
            return SetHeaderJoined(key, values.ToArray());
        }

        public OwinRequest SetHeaderUnmodified(string key, params string[] values)
        {
            Headers[key] = values;
            return this;
        }

        public OwinRequest SetHeaderUnmodified(string key, IEnumerable<string> values)
        {
            Headers[key] = values.ToArray();
            return this;
        }

        public OwinRequest AddHeader(string key, string value)
        {
            return AddHeaderUnmodified(key, value);
        }

        public OwinRequest AddHeaderJoined(string key, params string[] values)
        {
            var existing = GetHeaderUnmodified(key);
            return existing == null 
                ? SetHeaderJoined(key, values) 
                : SetHeaderJoined(key, existing.Concat(values));
        }

        public OwinRequest AddHeaderJoined(string key, IEnumerable<string> values)
        {
            var existing = GetHeaderUnmodified(key);
            return existing == null
                ? SetHeaderJoined(key, values)
                : SetHeaderJoined(key, existing.Concat(values));
        }

        public OwinRequest AddHeaderUnmodified(string key, params string[] values)
        {
            var existing = GetHeaderUnmodified(key);
            return existing == null
                ? SetHeaderUnmodified(key, values) 
                : SetHeaderUnmodified(key, existing.Concat(values));
        }
    }
}
