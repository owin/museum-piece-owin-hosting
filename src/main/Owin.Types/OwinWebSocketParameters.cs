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

namespace Owin.Types
{
    public partial struct OwinWebSocketParameters
    {
        public static OwinWebSocketParameters Create()
        {
            return new OwinWebSocketParameters(new ConcurrentDictionary<string, object>(StringComparer.Ordinal));
        }

        public static OwinWebSocketParameters Create(string subProtocol)
        {
            return new OwinWebSocketParameters(new ConcurrentDictionary<string, object>(StringComparer.Ordinal))
            {
                SubProtocol = subProtocol
            };
        }
    }
}
