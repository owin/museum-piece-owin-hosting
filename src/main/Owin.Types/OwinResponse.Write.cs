// <copyright file="OwinResponse.Write.cs" company="Microsoft Open Technologies, Inc.">
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
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Owin.Types
{
    public partial struct OwinResponse
    {
        public void Write(string text)
        {
            Write(text, Encoding.UTF8);
        }

        public void Write(string text, Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            byte[] buffer = encoding.GetBytes(text);
            Write(buffer, 0, buffer.Length);
        }

        public Task WriteAsync(string text)
        {
            return WriteAsync(text, Encoding.UTF8, CallCancelled);
        }

        public Task WriteAsync(string text, Encoding encoding)
        {
            return WriteAsync(text, encoding, CallCancelled);
        }

        public Task WriteAsync(string text, CancellationToken cancel)
        {
            return WriteAsync(text, Encoding.UTF8, cancel);
        }

        public Task WriteAsync(string text, Encoding encoding, CancellationToken cancel)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            byte[] buffer = encoding.GetBytes(text);
            return WriteAsync(buffer, 0, buffer.Length, cancel);
        }

        public void Write(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            Body.Write(buffer, 0, buffer.Length);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            Body.Write(buffer, offset, count);
        }

        public Task WriteAsync(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            return WriteAsync(buffer, 0, buffer.Length, CallCancelled);
        }

        public Task WriteAsync(byte[] buffer, int offset, int count)
        {
            return WriteAsync(buffer, offset, count, CallCancelled);
        }

        public Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancel)
        {
            if (cancel.IsCancellationRequested)
            {
                TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
                tcs.TrySetCanceled();
                return tcs.Task;
            }

            Stream body = Body;
            return Task.Factory.FromAsync(body.BeginWrite, body.EndWrite, buffer, offset, count, null);
            // 4.5: return Body.WriteAsync(buffer, offset, count, cancel);
        }
    }
}
