// <copyright file="OwinRequest.Spec-CommonKeys.cs" company="Microsoft Open Technologies, Inc.">
// Copyright 2013 Microsoft Open Technologies, Inc. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using Owin.Types.AppBuilder;

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

        public IPrincipal User
        {
            get { return Get<IPrincipal>(OwinConstants.CommonKeys.User); }
            set { Set(OwinConstants.CommonKeys.User, value); }
        }

        public Action<Action<object>, object> OnSendingHeaders
        {
            get { return Get<Action<Action<object>, object>>(OwinConstants.CommonKeys.OnSendingHeaders); }
            set { Set(OwinConstants.CommonKeys.OnSendingHeaders, value); }
        }

        public Capabilities Capabilities
        {
            get { return new Capabilities(Get<IDictionary<string, object>>(OwinConstants.CommonKeys.Capabilities)); }
            set { Set(OwinConstants.CommonKeys.Capabilities, value.Dictionary); }
        }
    }
}
