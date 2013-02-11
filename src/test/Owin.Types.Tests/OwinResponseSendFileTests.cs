// <copyright file="OwinResponseSendFileTests.cs" company="Microsoft Open Technologies, Inc.">
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

using System.Threading;
using Shouldly;
using Xunit;

namespace Owin.Types.Tests
{
    public class OwinResponseSendFileTests
    {
        [Fact]
        public void SendAsyncKeyDeterminesIfYouCanCallSendFileAsync()
        {
            var res = new OwinResponse(OwinRequest.Create());
            res.CanSendFile.ShouldBe(false);
            res.SendFileAsyncDelegate = (a, b, c, d) => null;
            res.CanSendFile.ShouldBe(true);
        }

        [Fact]
        public void CallingMethodInvokesDelegate()
        {
            var res = new OwinResponse(OwinRequest.Create());
            res.CanSendFile.ShouldBe(false);

            string aa = null;
            long bb = 0;
            long? cc = null;
            CancellationToken dd = CancellationToken.None;

            var cts = new CancellationTokenSource();

            res.SendFileAsyncDelegate = (a, b, c, d) =>
            {
                aa = a;
                bb = b;
                cc = c;
                dd = d;
                return null;
            };
            res.SendFileAsync("one", 2, 3, cts.Token);
            aa.ShouldBe("one");
            bb.ShouldBe(2);
            cc.ShouldBe(3);
            dd.ShouldBe(cts.Token);

            res.SendFileAsync("four");
            aa.ShouldBe("four");
            bb.ShouldBe(0);
            cc.ShouldBe(null);
            dd.ShouldBe(CancellationToken.None);
        }
    }
}
