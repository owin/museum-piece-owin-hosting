// <copyright file="OwinResponse.Spec-Security.cs" company="Microsoft Open Technologies, Inc.">
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
using System.IdentityModel.Claims;
using System.Security.Principal;

namespace Owin.Types
{
    public partial struct OwinResponse
    {
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "Following Owin conventions.")]
        public Tuple<IPrincipal, IDictionary<string, object>> SignIn
        {
            get { return Get<Tuple<IPrincipal, IDictionary<string, object>>>(OwinConstants.Security.SignIn); }
            set { Set(OwinConstants.Security.SignIn, value); }
        }

        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays",
            Justification = "Using an array rather than a collection for this property for performance reasons.")]
        public string[] SignOut
        {
            get { return Get<string[]>(OwinConstants.Security.SignOut); }
            set { Set(OwinConstants.Security.SignOut, value); }
        }

        public Tuple<string[], Claim[]> Challenge
        {
            get { return Get<Tuple<string[], Claim[]>>(OwinConstants.Security.Challenge); }
            set { Set(OwinConstants.Security.Challenge, value); }
        }
    }
}
