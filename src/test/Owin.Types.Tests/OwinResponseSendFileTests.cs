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
