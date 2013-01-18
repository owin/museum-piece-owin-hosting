using System.Collections.Generic;

namespace Owin.Types
{
    public partial struct OwinResponse
    {
        public OwinResponse(OwinRequest request)
        {
            _dictionary = request.Dictionary;
        }
    }
}
