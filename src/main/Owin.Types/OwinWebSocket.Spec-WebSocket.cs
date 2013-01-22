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

using System.Threading;
using SendAsyncDelegate = System.Func<System.ArraySegment<byte>, int, bool, System.Threading.CancellationToken, System.Threading.Tasks.Task>;
using ReceiveAsyncDelegate = System.Func<System.ArraySegment<byte>, System.Threading.CancellationToken, System.Threading.Tasks.Task<System.Tuple<int, bool, int>>>;
using CloseAsyncDelegate = System.Func<int, string, System.Threading.CancellationToken, System.Threading.Tasks.Task>;

namespace Owin.Types
{
    public partial struct OwinWebSocket
    {
        public SendAsyncDelegate SendAsyncDelegate
        {
            get { return Get<SendAsyncDelegate>(OwinConstants.WebSocket.SendAsync); }
            set { Set(OwinConstants.WebSocket.SendAsync, value); }
        }

        public ReceiveAsyncDelegate ReceiveAsyncDelegate
        {
            get { return Get<ReceiveAsyncDelegate>(OwinConstants.WebSocket.ReceiveAsync); }
            set { Set(OwinConstants.WebSocket.ReceiveAsync, value); }
        }

        public CloseAsyncDelegate CloseAsyncDelegate
        {
            get { return Get<CloseAsyncDelegate>(OwinConstants.WebSocket.CloseAsync); }
            set { Set(OwinConstants.WebSocket.CloseAsync, value); }
        }

        public string Version
        {
            get { return Get<string>(OwinConstants.WebSocket.Version); }
            set { Set(OwinConstants.WebSocket.Version, value); }
        }

        public CancellationToken CallCancelled
        {
            get { return Get<CancellationToken>(OwinConstants.WebSocket.CallCancelled); }
            set { Set(OwinConstants.WebSocket.CallCancelled, value); }
        }

        public int ClientCloseStatus
        {
            get { return Get<int>(OwinConstants.WebSocket.ClientCloseStatus); }
            set { Set(OwinConstants.WebSocket.ClientCloseStatus, value); }
        }

        public string ClientCloseDescription
        {
            get { return Get<string>(OwinConstants.WebSocket.ClientCloseDescription); }
            set { Set(OwinConstants.WebSocket.ClientCloseDescription, value); }
        }
    }
}
