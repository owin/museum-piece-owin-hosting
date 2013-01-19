using System;
using System.Collections.Concurrent;

namespace Owin.Types
{
    public partial struct OwinWebSocketParameters
    {
        public string SubProtocol
        {
            get { return Get<string>(OwinConstants.WebSocket.SubProtocol); }
            set { Set(OwinConstants.WebSocket.SubProtocol, value); }
        }
    }
}
