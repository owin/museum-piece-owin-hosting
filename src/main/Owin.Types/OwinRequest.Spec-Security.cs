// <copyright file="OwinRequest.Spec-Security.cs" company="Microsoft Open Technologies, Inc.">
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
using System.Security.Principal;
using System.Threading.Tasks;
using GetIdentitiesDelegate = System.Func<string[], System.Action<System.Security.Principal.IIdentity, object>, object, System.Threading.Tasks.Task>;

namespace Owin.Types
{
    public partial struct OwinRequest
    {
        private static readonly Action<IIdentity, object> CallbackDelegate = (identity, state) => ((Action<IIdentity>)state).Invoke(identity);

        public IPrincipal User
        {
            get { return Get<IPrincipal>(OwinConstants.Security.User); }
            set { Set(OwinConstants.Security.User, value); }
        }

        public GetIdentitiesDelegate GetIdentitiesDelegate
        {
            get { return Get<GetIdentitiesDelegate>(OwinConstants.Security.GetIdentities); }
            set { Set(OwinConstants.Security.GetIdentities, value); }
        }

        public Task GetIdentities(string[] authenticationTypes, Action<IIdentity, object> callback, object state)
        {
            return GetIdentitiesDelegate.Invoke(authenticationTypes, callback, state);
        }

        public Task GetIdentities(string[] authenticationTypes, Action<IIdentity> callback)
        {
            return GetIdentitiesDelegate.Invoke(authenticationTypes, CallbackDelegate, callback);
        }
    }
}
