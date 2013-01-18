using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Owin.Types
{
    using AcceptDelegate = Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>>;

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
            IDictionary<string, object> parameters,
            Func<OwinWebSocket, Task> callback)
        {
            AcceptDelegate.Invoke(parameters, webSocket => callback(new OwinWebSocket(webSocket)));
        }
    }
}
