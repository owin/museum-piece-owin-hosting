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
