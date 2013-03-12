// <copyright file="Address.cs" company="Microsoft Open Technologies, Inc.">
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

using System.Collections.Generic;

namespace Owin.Types.AppBuilder
{
    public partial struct Address
    {
        public Address(string scheme, string host, string port, string path)
            : this(new Dictionary<string, object>())
        {
            Scheme = scheme;
            Host = host;
            Port = port;
            Path = path;
        }

        public string Scheme
        {
            get { return Get<string>(OwinConstants.CommonKeys.Scheme); }
            set { Set(OwinConstants.CommonKeys.Scheme, value); }
        }

        public string Host
        {
            get { return Get<string>(OwinConstants.CommonKeys.Host); }
            set { Set(OwinConstants.CommonKeys.Host, value); }
        }

        public string Port
        {
            get { return Get<string>(OwinConstants.CommonKeys.Port); }
            set { Set(OwinConstants.CommonKeys.Port, value); }
        }

        public string Path
        {
            get { return Get<string>(OwinConstants.CommonKeys.Path); }
            set { Set(OwinConstants.CommonKeys.Path, value); }
        }

        public static Address Create()
        {
            return new Address(new Dictionary<string, object>());
        }
    }
}
