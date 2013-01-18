using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Owin.Types
{
    public partial struct OwinResponse
    {
        public string OwinVersion
        {
            get { return Get<string>(OwinConstants.OwinVersion); }
            set { Set(OwinConstants.OwinVersion, value); }
        }

        public CancellationToken CallCancelled
        {
            get { return Get<CancellationToken>(OwinConstants.CallCancelled); }
            set { Set(OwinConstants.CallCancelled, value); }
        }

        public int StatusCode
        {
            get { return Get<int>(OwinConstants.ResponseStatusCode); }
            set { Set(OwinConstants.ResponseStatusCode, value); }
        }

        public string ReasonPhrase
        {
            get { return Get<string>(OwinConstants.ResponseReasonPhrase); }
            set { Set(OwinConstants.ResponseReasonPhrase, value); }
        }

        public string Protocol
        {
            get { return Get<string>(OwinConstants.ResponseProtocol); }
            set { Set(OwinConstants.ResponseProtocol, value); }
        }

        public IDictionary<string, string[]> Headers
        {
            get { return Get<IDictionary<string, string[]>>(OwinConstants.ResponseHeaders); }
            set { Set(OwinConstants.ResponseHeaders, value); }
        }

        public Stream Body
        {
            get { return Get<Stream>(OwinConstants.ResponseBody); }
            set { Set(OwinConstants.ResponseBody, value); }
        }
    }
}
