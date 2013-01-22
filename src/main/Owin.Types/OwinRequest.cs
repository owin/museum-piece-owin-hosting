// Licensed to Monkey Square, Inc. under one or more contributor 
// license agreements.  See the NOTICE file distributed with 
// this work or additional information regarding copyright 
// ownership.  Monkey Square, Inc. licenses this file to you 
// under the Apache License, Version 2.0 (the "License"); you 
// may not use this file except in compliance with the License.
// You may obtain a copy of the License at 
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.

using System;
using System.Collections.Concurrent;
using Owin.Types.Helpers;

namespace Owin.Types
{
    public partial struct OwinRequest
    {
        public static OwinRequest Create()
        {
            var environment = new ConcurrentDictionary<string, object>(StringComparer.Ordinal);
            environment[OwinConstants.RequestHeaders] =
                new ConcurrentDictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            environment[OwinConstants.ResponseHeaders] =
                new ConcurrentDictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            return new OwinRequest(environment);
        }

        public string Host
        {
            get { return OwinHelpers.GetHost(this); }
            set { SetHeader("Host", value); }
        }

        public Uri Uri
        {
            get { return OwinHelpers.GetUri(this); }
        }
    }
}
