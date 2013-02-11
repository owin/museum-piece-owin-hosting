// <copyright file="OwinHelpers.Header.cs" company="Microsoft Open Technologies, Inc.">
// Copyright 2013 Microsoft Open Technologies, Inc. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

namespace Owin.Types.Helpers
{
    public static partial class OwinHelpers
    {
        public static string GetHeader(IDictionary<string, string[]> headers, string key)
        {
            string[] values = GetHeaderUnmodified(headers, key);
            return values == null ? null : string.Join(",", values);
        }

        public static IEnumerable<string> GetHeaderSplit(IDictionary<string, string[]> headers, string key)
        {
            string[] values = GetHeaderUnmodified(headers, key);
            return values == null ? null : GetHeaderSplitImplementation(values);
        }

        private static IEnumerable<string> GetHeaderSplitImplementation(string[] values)
        {
            foreach (var segment in new HeaderSegmentCollection(values))
            {
                if (segment.Data.HasValue)
                {
                    yield return segment.Data.Value;
                }
            }
        }

        public static string[] GetHeaderUnmodified(IDictionary<string, string[]> headers, string key)
        {
            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }
            string[] values;
            return headers.TryGetValue(key, out values) ? values : null;
        }

        public static void SetHeader(IDictionary<string, string[]> headers, string key, string value)
        {
            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }
            headers[key] = new[] { value };
        }

        public static void SetHeaderJoined(IDictionary<string, string[]> headers, string key, params string[] values)
        {
            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }
            headers[key] = new[] { string.Join(",", values) };
        }

        public static void SetHeaderJoined(IDictionary<string, string[]> headers, string key, IEnumerable<string> values)
        {
            SetHeaderJoined(headers, key, values.ToArray());
        }

        public static void SetHeaderUnmodified(IDictionary<string, string[]> headers, string key, params string[] values)
        {
            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }
            headers[key] = values;
        }

        public static void SetHeaderUnmodified(IDictionary<string, string[]> headers, string key, IEnumerable<string> values)
        {
            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }
            headers[key] = values.ToArray();
        }

        public static void AddHeader(IDictionary<string, string[]> headers, string key, string value)
        {
            AddHeaderUnmodified(headers, key, value);
        }

        public static void AddHeaderJoined(IDictionary<string, string[]> headers, string key, params string[] values)
        {
            var existing = GetHeaderUnmodified(headers, key);
            if (existing == null)
            {
                SetHeaderJoined(headers, key, values);
            }
            else
            {
                SetHeaderJoined(headers, key, existing.Concat(values));
            }
        }

        public static void AddHeaderJoined(IDictionary<string, string[]> headers, string key, IEnumerable<string> values)
        {
            var existing = GetHeaderUnmodified(headers, key);
            SetHeaderJoined(headers, key, existing == null ? values : existing.Concat(values));
        }

        public static void AddHeaderUnmodified(IDictionary<string, string[]> headers, string key, params string[] values)
        {
            var existing = GetHeaderUnmodified(headers, key);
            if (existing == null)
            {
                SetHeaderUnmodified(headers, key, values);
            }
            else
            {
                SetHeaderUnmodified(headers, key, existing.Concat(values));
            }
        }

        public static void AddHeaderUnmodified(IDictionary<string, string[]> headers, string key, IEnumerable<string> values)
        {
            var existing = GetHeaderUnmodified(headers, key);
            SetHeaderUnmodified(headers, key, existing == null ? values : existing.Concat(values));
        }
    }
}
