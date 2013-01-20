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
using SendFileAsyncDelegate = System.Func<string, long, long?, System.Threading.CancellationToken, System.Threading.Tasks.Task>;

namespace Owin.Types
{
    public partial struct OwinResponse
    {
        public bool CanSendFile
        {
            get { return SendFileAsyncDelegate != null; }
        }

        public SendFileAsyncDelegate SendFileAsyncDelegate
        {
            get { return Get<SendFileAsyncDelegate>(OwinConstants.SendFiles.SendAsync); }
            set { Set(OwinConstants.SendFiles.SendAsync, value); }
        }

        public Task SendFileAsync(string filePath, long offset, long? count, CancellationToken cancel)
        {
            var sendFile = SendFileAsyncDelegate;
            if (sendFile == null)
            {
                throw new NotSupportedException(OwinConstants.SendFiles.SendAsync);
            }
            return sendFile.Invoke(filePath, offset, count, cancel);
        }

        public Task SendFileAsync(string filePath, long offset, long? count)
        {
            return SendFileAsync(filePath, offset, count, CancellationToken.None);
        }

        public Task SendFileAsync(string filePath, CancellationToken cancel)
        {
            return SendFileAsync(filePath, 0, null, cancel);
        }

        public Task SendFileAsync(string filePath)
        {
            return SendFileAsync(filePath, 0, null, CancellationToken.None);
        }
    }
}
