// <copyright file="Capabilities.cs" company="Microsoft Open Technologies, Inc.">
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

using System.Collections.Generic;

namespace Owin.Types.AppBuilder
{
    public partial struct Capabilities
    {
        public string SendFileVersion
        {
            get { return Get<string>(OwinConstants.SendFiles.Version); }
            set { Set(OwinConstants.SendFiles.Version, value); }
        }

        // TODO: sendfile.Support IDictionary<string, object> containing sendfile.Concurrency. Only supported by HttpSys.

        public string OpaqueVersion
        {
            get { return Get<string>(OwinConstants.OpaqueConstants.Version); }
            set { Set(OwinConstants.OpaqueConstants.Version, value); }
        }

        public string WebSocketVersion
        {
            get { return Get<string>(OwinConstants.WebSocket.Version); }
            set { Set(OwinConstants.WebSocket.Version, value); }
        }

        public static Capabilities Create()
        {
            return new Capabilities(new Dictionary<string, object>());
        }
    }
}
