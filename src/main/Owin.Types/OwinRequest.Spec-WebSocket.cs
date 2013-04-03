// <copyright file="OwinRequest.Spec-WebSocket.cs" company="Microsoft Open Technologies, Inc.">
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
using System.Threading.Tasks;
using Owin.Types.WebSockets;
using AcceptDelegate = System.Action<System.Collections.Generic.IDictionary<string, object>, System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>>;

namespace Owin.Types
{
    public partial struct OwinRequest
    {
        public bool CanAccept
        {
            get { return AcceptDelegate != null; }
        }

        public AcceptDelegate AcceptDelegate
        {
            get { return Get<AcceptDelegate>(OwinConstants.WebSocket.Accept); }
        }

        public void Accept(
            OwinWebSocketParameters parameters,
            Func<OwinWebSocket, Task> callback)
        {
            var accept = AcceptDelegate;
            if (accept == null)
            {
                throw new NotSupportedException(OwinConstants.WebSocket.Accept);
            }
            accept.Invoke(
                parameters.Dictionary,
                webSocket => callback(new OwinWebSocket(webSocket)));
        }

        public void Accept(
            string subProtocol,
            Func<OwinWebSocket, Task> callback)
        {
            Accept(OwinWebSocketParameters.Create(subProtocol), callback);
        }

        public void Accept(
            Func<OwinWebSocket, Task> callback)
        {
            Accept(OwinWebSocketParameters.Create(), callback);
        }
    }
}
