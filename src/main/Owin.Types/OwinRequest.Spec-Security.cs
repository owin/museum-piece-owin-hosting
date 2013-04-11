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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.Threading.Tasks;
using AuthenticateCallbackDelegate = System.Action<System.Security.Principal.IPrincipal, System.Collections.Generic.IDictionary<string, object>, System.Collections.Generic.IDictionary<string, object>, object>;
using AuthenticateDelegate = System.Func<string[], System.Action<System.Security.Principal.IPrincipal, System.Collections.Generic.IDictionary<string, object>, System.Collections.Generic.IDictionary<string, object>, object>, object, System.Threading.Tasks.Task>;

namespace Owin.Types
{
    public partial struct OwinRequest
    {
        private static readonly AuthenticateCallbackDelegate AuthenticatePrincipalExtraPropertiesDelegate = (principal, extra, properties, state) => ((Action<IPrincipal, IDictionary<string, object>, IDictionary<string, object>>)state).Invoke(principal, extra, properties);
        private static readonly AuthenticateCallbackDelegate AuthenticatePrincipalExtraDelegate = (principal, extra, properties, state) => ((Action<IPrincipal, IDictionary<string, object>>)state).Invoke(principal, extra);
        private static readonly AuthenticateCallbackDelegate AuthenticatePrincipalDelegate = (principal, extra, properties, state) => ((Action<IPrincipal>)state).Invoke(principal);
        private static readonly AuthenticateCallbackDelegate GetAuthenticationTypesExtraDelegate = (principal, extra, properties, state) => ((Action<IDictionary<string, object>>)state).Invoke(extra);

        public IPrincipal User
        {
            get { return Get<IPrincipal>(OwinConstants.Security.User); }
            set { Set(OwinConstants.Security.User, value); }
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "Following Owin conventions.")]
        public AuthenticateDelegate AuthenticateDelegate
        {
            get { return Get<AuthenticateDelegate>(OwinConstants.Security.Authenticate); }
            set { Set(OwinConstants.Security.Authenticate, value); }
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "Following Owin conventions.")]
        public Task GetAuthenticationTypes(Action<IDictionary<string, object>, object> callback, object state)
        {
            return AuthenticateDelegate.Invoke(null, (ignore1, ignore2, extra, innerState) =>
                callback.Invoke(extra, innerState), state);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "Following Owin conventions.")]
        public Task GetAuthenticationTypes(Action<IDictionary<string, object>, object> callback)
        {
            return AuthenticateDelegate.Invoke(null, GetAuthenticationTypesExtraDelegate, callback);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "Following Owin conventions.")]
        public Task Authenticate(string[] authenticationTypes, Action<IPrincipal, IDictionary<string, object>,
            IDictionary<string, object>, object> callback, object state)
        {
            return AuthenticateDelegate.Invoke(authenticationTypes, callback, state);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "Following Owin conventions.")]
        public Task Authenticate(string[] authenticationTypes, Action<IPrincipal, IDictionary<string, object>,
            IDictionary<string, object>> callback)
        {
            return AuthenticateDelegate.Invoke(authenticationTypes, AuthenticatePrincipalExtraPropertiesDelegate, callback);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "Following Owin conventions.")]
        public Task Authenticate(string[] authenticationTypes, Action<IPrincipal,
            IDictionary<string, object>> callback)
        {
            return AuthenticateDelegate.Invoke(authenticationTypes, AuthenticatePrincipalExtraDelegate, callback);
        }

        public Task Authenticate(string[] authenticationTypes, Action<IPrincipal> callback)
        {
            return AuthenticateDelegate.Invoke(authenticationTypes, AuthenticatePrincipalDelegate, callback);
        }
    }
}
