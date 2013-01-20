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
using System.Threading;
using System.Threading.Tasks;

namespace Owin.Types
{
    public partial struct OwinWebSocket
    {
        public Task SendAsync(ArraySegment<byte> data, int messageType, bool endOfMessage, CancellationToken cancel)
        {
            return SendAsyncDelegate.Invoke(data, messageType, endOfMessage, cancel);
        }

        public Task<OwinWebSocketReceiveMessage> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancel)
        {
            //TODO: avoid ContinueWith when completed synchronously
            return ReceiveAsyncDelegate.Invoke(buffer, cancel)
                .ContinueWith(tuple => new OwinWebSocketReceiveMessage(tuple.Result));
        }

        public Task CloseAsync(int closeStatus, string closeDescription, CancellationToken cancel)
        {
            return CloseAsyncDelegate.Invoke(closeStatus, closeDescription, cancel);
        }

        public Task CloseAsync(int closeStatus, CancellationToken cancel)
        {
            return CloseAsyncDelegate.Invoke(closeStatus, null, cancel);
        }

        public Task CloseAsync(CancellationToken cancel)
        {
            return CloseAsyncDelegate.Invoke(0, null, cancel);
        }
    }
}
