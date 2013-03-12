// <copyright file="AppPropertiesTests.cs" company="Microsoft Open Technologies, Inc.">
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
using Owin.Types.AppBuilder;
using Xunit;

namespace Owin.Types.Tests
{
    public class AppPropertiesTests
    {
        [Fact]
        public void AddressCollectionOperations()
        {
            IDictionary<string, object> properties = new Dictionary<string, object>();
            AppProperties appProperties = new AppProperties(properties);
            appProperties.Addresses = AddressCollection.Create();
            appProperties.Addresses.Add(new Address("http", "*", "80", "/"));

            Assert.Equal(1, appProperties.Addresses.Count);
            Assert.Equal("http", appProperties.Addresses[0].Scheme);
            
            foreach (Address address in appProperties.Addresses)
            {
                Assert.Equal("http", address.Scheme);
                Assert.Equal("*", address.Host);
                Assert.Equal("80", address.Port);
                Assert.Equal("/", address.Path);
            }
        }

        [Fact]
        public void CapabilitiesOperations()
        {
            IDictionary<string, object> properties = new Dictionary<string, object>();
            AppProperties appProperties = new AppProperties(properties);
            appProperties.Capabilities = Capabilities.Create();
            Capabilities capabilities = appProperties.Capabilities;
            capabilities.OpaqueVersion = "1.0";
            capabilities.SendFileVersion = "1.1";
            capabilities.WebSocketVersion = "1.2";
            Assert.Equal("1.0", appProperties.Capabilities.OpaqueVersion);
            Assert.Equal("1.1", appProperties.Capabilities.SendFileVersion);
            Assert.Equal("1.2", appProperties.Capabilities.WebSocketVersion);
        }
    }
}
