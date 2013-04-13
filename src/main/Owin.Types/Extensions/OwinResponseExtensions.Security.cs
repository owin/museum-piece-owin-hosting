// <copyright file="OwinRequestExtensions.Cookies.cs" company="Microsoft Open Technologies, Inc.">
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
using System.Security.Principal;

namespace Owin.Types.Extensions
{
    public static partial class OwinResponseExtensions
    {
        public static void SignIn(this OwinResponse response, IPrincipal user)
        {
            SignIn(response, user, null);
        }

        public static void SignIn(this OwinResponse response, IPrincipal user, IDictionary<string, string> extra)
        {
            response.SignIn = new Tuple<IPrincipal, IDictionary<string, string>>(user, extra);
        }

        public static void SignOut(this OwinResponse response, params string[] authenticationTypes)
        {
            response.SignOut = authenticationTypes;
        }

        public static void Unauthorized(this OwinResponse response, params string[] authenticationTypes)
        {
            Unauthorized(response, authenticationTypes, null);
        }

        public static void Unauthorized(this OwinResponse response, string[] authenticationTypes, IDictionary<string, string> extra)
        {
            response.StatusCode = 401;
            response.Challenge = Tuple.Create(authenticationTypes, extra);
        }
    }
}
