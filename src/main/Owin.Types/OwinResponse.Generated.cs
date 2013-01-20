using System;
using System.Linq;
using System.Collections.Generic;

namespace Owin.Types
{
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

        public OwinResponse SetHeader(string key, string value)
        {
            Headers[key] = new[] { value };
            return this;
        }

        public OwinResponse SetHeaderJoined(string key, params string[] values)
        {
            Headers[key] = new[] { string.Join(",", values) };
            return this;
        }

        public OwinResponse SetHeaderJoined(string key, IEnumerable<string> values)
        {
            return SetHeaderJoined(key, values.ToArray());
        }

        public OwinResponse SetHeaderUnmodified(string key, params string[] values)
        {
            Headers[key] = values;
            return this;
        }

        public OwinResponse SetHeaderUnmodified(string key, IEnumerable<string> values)
        {
            Headers[key] = values.ToArray();
            return this;
        }

        public OwinResponse AddHeader(string key, string value)
        {
            return AddHeaderUnmodified(key, value);
        }

        public OwinResponse AddHeaderJoined(string key, params string[] values)
        {
            var existing = GetHeaderUnmodified(key);
            return existing == null 
                ? SetHeaderJoined(key, values) 
                : SetHeaderJoined(key, existing.Concat(values));
        }

        public OwinResponse AddHeaderJoined(string key, IEnumerable<string> values)
        {
            var existing = GetHeaderUnmodified(key);
            return existing == null
                ? SetHeaderJoined(key, values)
                : SetHeaderJoined(key, existing.Concat(values));
        }

        public OwinResponse AddHeaderUnmodified(string key, params string[] values)
        {
            var existing = GetHeaderUnmodified(key);
            return existing == null
                ? SetHeaderUnmodified(key, values) 
                : SetHeaderUnmodified(key, existing.Concat(values));
        }
    }
}
