// <copyright file="OwinHelpers.Query.cs" company="Microsoft Open Technologies, Inc.">
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
        private static readonly Action<string, string, object> AddQueryCallback = (name, value, state) =>
        {
            var dictionary = (IDictionary<string, string[]>)state;

            string[] existing;
            if (!dictionary.TryGetValue(name, out existing))
            {
                dictionary.Add(name, new[] { value });
            }
            else
            {
                dictionary[name] = existing.Concat(new[] { value }).ToArray();
            }
        };

        private static readonly char[] AmpersandAndSemicolon = new[] { '&', ';' };

        public static IDictionary<string, string[]> GetQuery(OwinRequest request)
        {
            var query = request.Get<IDictionary<string, string[]>>("Owin.Types.Query#dictionary");
            if (query == null)
            {
                query = new Dictionary<string, string[]>(StringComparer.Ordinal);
                request.Set("Owin.Types.Query#dictionary", query);
            }

            var text = request.QueryString;
            if (request.Get<string>("Owin.Types.Query#text") != text)
            {
                query.Clear();
                ParseDelimited(text, AmpersandAndSemicolon, AddQueryCallback, query);
                request.Set("Owin.Types.Query#text", text);
            }
            return query;
        }
    }
}
