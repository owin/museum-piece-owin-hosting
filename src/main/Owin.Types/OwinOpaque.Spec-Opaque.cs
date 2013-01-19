using System.IO;
using System.Threading;

namespace Owin.Types
{
    public partial struct OwinOpaque
    {
        public string Version
        {
            get { return Get<string>(OwinConstants.Opaque.Version); }
            set { Set(OwinConstants.Opaque.Version, value); }
        }

        public CancellationToken CallCancelled
        {
            get { return Get<CancellationToken>(OwinConstants.Opaque.CallCancelled); }
            set { Set(OwinConstants.Opaque.CallCancelled, value); }
        }

        public Stream Stream
        {
            get { return Get<Stream>(OwinConstants.Opaque.Stream); }
            set { Set(OwinConstants.Opaque.Stream, value); }
        }
    }
}
