using System;
using System.Threading.Tasks;

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
