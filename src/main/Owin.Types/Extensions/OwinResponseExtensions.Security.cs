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

using System.Linq;
using System.Security.Principal;

namespace Owin.Types.Extensions
{
    public static partial class OwinResponseExtensions
    {
        public static void SignIn(this OwinResponse response, IPrincipal user)
        {
            response.Grant = user;
        }

        // public static void SignOut(this OwinResponse response, params string[] authenticationTypes)
        // {
            // This is all incorrect
            // var signout = new ClaimsPrincipal();
            // if (authenticationTypes != null && authenticationTypes.Length != 0)
            // {
            //    signout.AddIdentities(authenticationTypes.Select(
            //        authenticationType => new ClaimsIdentity(authenticationType)));
            // }
            // response.Set("security.Signout", signout);
        // }

        // public static void Unauthorized(this OwinResponse response, params string[] authenticationTypes)
        // {
            // This is all incorrect
            // var challenge = new ClaimsPrincipal();
            // if (authenticationTypes != null && authenticationTypes.Length != 0)
            // {
            //    challenge.AddIdentities(authenticationTypes.Select(
            //        authenticationType => new ClaimsIdentity(authenticationType)));
            // }
            // Unauthorized(response, challenge);
        // }

        // public static void Unauthorized(this OwinResponse response, IPrincipal challenge)
        // {
        //    response.StatusCode = 401;
        //    response.Challenge = challenge;
        // }
    }
}
