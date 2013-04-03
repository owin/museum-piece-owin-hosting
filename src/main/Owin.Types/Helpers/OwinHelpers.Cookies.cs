// <copyright file="OwinHelpers.Uri.cs" company="Microsoft Open Technologies, Inc.">
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
using System.Globalization;
using System.Linq;

namespace Owin.Types.Helpers
{
    public static partial class OwinHelpers
    {
        private static readonly Action<string, string, object> AddCookieCallback = (name, value, state) =>
        {
            var dictionary = (IDictionary<string, string>)state;
            if (!dictionary.ContainsKey(name))
            {
                dictionary.Add(name, value);
            }
        };

        private static readonly char[] SemicolonAndComma = new[] { ';', ',' };

        public static IDictionary<string, string> GetCookies(OwinRequest request)
        {
            var cookies = request.Get<IDictionary<string, string>>("Owin.Types.Cookies#dictionary");
            if (cookies == null)
            {
                cookies = new Dictionary<string, string>(StringComparer.Ordinal);
                request.Set("Owin.Types.Cookies#dictionary", cookies);
            }

            var text = request.GetHeader("Cookie");
            if (request.Get<string>("Owin.Types.Cookies#text") != text)
            {
                cookies.Clear();
                ParseDelimited(text, SemicolonAndComma, AddCookieCallback, cookies);
                request.Set("Owin.Types.Cookies#text", text);
            }
            return cookies;
        }

        public static void AddCookie(OwinResponse response, string key, string value)
        {
            response.AddHeader("Set-Cookie", Uri.EscapeDataString(key) + "=" + Uri.EscapeDataString(value) + "; path=/");
        }

        public static void AddCookie(OwinResponse response, string key, string value, CookieOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            var domainHasValue = !string.IsNullOrEmpty(options.Domain);
            var pathHasValue = !string.IsNullOrEmpty(options.Path);
            var expiresHasValue = options.Expires.HasValue;

            var setCookieValue = string.Concat(
                Uri.EscapeDataString(key),
                "=",
                Uri.EscapeDataString(value ?? string.Empty),
                !domainHasValue ? null : "; domain=",
                !domainHasValue ? null : options.Domain,
                !pathHasValue ? null : "; path=",
                !pathHasValue ? null : options.Path,
                !expiresHasValue ? null : "; expires=",
                !expiresHasValue ? null : options.Expires.Value.ToString("ddd, dd-MMM-yyyy HH:mm:ss ", CultureInfo.InvariantCulture) + "GMT",
                !options.Secure ? null : "; secure",
                !options.HttpOnly ? null : "; HttpOnly");
            response.AddHeader("Set-Cookie", setCookieValue);
        }

        public static void DeleteCookie(OwinResponse response, string key)
        {
            Func<string, bool> predicate = value => value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase);

            var deleteCookies = new[] { Uri.EscapeDataString(key) + "=; expires=Thu, 01-Jan-1970 00:00:00 GMT" };
            var existingValues = response.GetHeaderUnmodified("Set-Cookie");
            if (existingValues == null || existingValues.Length == 0)
            {
                response.SetHeaderUnmodified("Set-Cookie", deleteCookies);
            }
            else
            {
                response.SetHeaderUnmodified("Set-Cookie", existingValues.Where(value => !predicate(value)).Concat(deleteCookies).ToArray());
            }
        }

        public static void DeleteCookie(OwinResponse response, string key, CookieOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            var domainHasValue = !string.IsNullOrEmpty(options.Domain);
            var pathHasValue = !string.IsNullOrEmpty(options.Path);

            Func<string, bool> rejectPredicate;
            if (domainHasValue)
            {
                rejectPredicate = value =>
                    value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase) &&
                        value.IndexOf("domain=" + options.Domain, StringComparison.OrdinalIgnoreCase) != -1;
            }
            else if (pathHasValue)
            {
                rejectPredicate = value =>
                    value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase) &&
                        value.IndexOf("path=" + options.Path, StringComparison.OrdinalIgnoreCase) != -1;
            }
            else
            {
                rejectPredicate = value => value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase);
            }
            var existingValues = response.GetHeaderUnmodified("Set-Cookie");
            if (existingValues != null)
            {
                response.SetHeaderUnmodified("Set-Cookie", existingValues.Where(value => !rejectPredicate(value)).ToArray());
            }

            AddCookie(response, key, string.Empty, new CookieOptions
            {
                Path = options.Path,
                Domain = options.Domain,
                Expires = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            });
        }

        private static void ParseDelimited(string text, char[] delimiters, Action<string, string, object> callback, object state)
        {
            var textLength = text.Length;
            var equalIndex = text.IndexOf('=');
            if (equalIndex == -1)
            {
                equalIndex = textLength;
            }
            var scanIndex = 0;
            while (scanIndex < textLength)
            {
                var delimiterIndex = text.IndexOfAny(delimiters, scanIndex);
                if (delimiterIndex == -1)
                {
                    delimiterIndex = textLength;
                }
                if (equalIndex < delimiterIndex)
                {
                    while (scanIndex != equalIndex && char.IsWhiteSpace(text[scanIndex]))
                    {
                        ++scanIndex;
                    }
                    var name = text.Substring(scanIndex, equalIndex - scanIndex);
                    var value = text.Substring(equalIndex + 1, delimiterIndex - equalIndex - 1);
                    callback(
                        Uri.UnescapeDataString(name),
                        Uri.UnescapeDataString(value),
                        state);
                    equalIndex = text.IndexOf('=', equalIndex + 1);
                    if (equalIndex == -1)
                    {
                        equalIndex = textLength;
                    }
                }
                scanIndex = delimiterIndex + 1;
            }
        }
    }
}
