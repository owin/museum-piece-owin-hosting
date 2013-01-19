using System;
using System.Threading;
using System.Threading.Tasks;

namespace Owin.Types
{
    using SendAsyncDelegate = Func<string, long, long?, CancellationToken, Task>;
    using ReceiveAsyncDelegate = Func<string, long, long?, CancellationToken, Task>;
    using CloseAsyncDelegate = Func<string, long, long?, CancellationToken, Task>;

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

        public CloseAsyncDelegate Version
        {
            get { return Get<CloseAsyncDelegate>(OwinConstants.WebSocket.Version); }
            set { Set(OwinConstants.WebSocket.Version, value); }
        }

        public CloseAsyncDelegate CallCancelled
        {
            get { return Get<CloseAsyncDelegate>(OwinConstants.WebSocket.CallCancelled); }
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
