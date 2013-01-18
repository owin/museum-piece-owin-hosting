using System;
using System.IO;

namespace Owin.Types
{
    public partial struct OwinRequest
    {
        public string RemoteIpAddress
        {
            get { return Get<string>(OwinConstants.CommonKeys.RemoteIpAddress); }
            set { Set(OwinConstants.CommonKeys.RemoteIpAddress, value); }
        }

        public string RemotePort
        {
            get { return Get<string>(OwinConstants.CommonKeys.RemotePort); }
            set { Set(OwinConstants.CommonKeys.RemotePort, value); }
        }

        public string LocalIpAddress
        {
            get { return Get<string>(OwinConstants.CommonKeys.LocalIpAddress); }
            set { Set(OwinConstants.CommonKeys.LocalIpAddress, value); }
        }

        public string LocalPort
        {
            get { return Get<string>(OwinConstants.CommonKeys.LocalPort); }
            set { Set(OwinConstants.CommonKeys.LocalPort, value); }
        }

        public bool IsLocal
        {
            get { return Get<bool>(OwinConstants.CommonKeys.IsLocal); }
            set { Set(OwinConstants.CommonKeys.IsLocal, value); }
        }

        public TextWriter TraceOutput
        {
            get { return Get<TextWriter>(OwinConstants.CommonKeys.TraceOutput); }
            set { Set(OwinConstants.CommonKeys.TraceOutput, value); }
        }

        public Action<Action<object>, object> OnSendingHeaders
        {
            get { return Get<Action<Action<object>, object>>(OwinConstants.CommonKeys.OnSendingHeaders); }
            set { Set(OwinConstants.CommonKeys.OnSendingHeaders, value); }
        }
    }
}
