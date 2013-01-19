using System;

namespace Owin.Types
{
    public partial struct OwinWebSocketReceiveMessage
    {
        private readonly Tuple<int, bool, int> _tuple;

        public OwinWebSocketReceiveMessage(Tuple<int, bool, int> tuple)
        {
            _tuple = tuple;
        }

        public int MessageType { get { return _tuple.Item1; } }
        public bool EndOfMessage { get { return _tuple.Item2; } }
        public int Count { get { return _tuple.Item3; } }
    }
}
