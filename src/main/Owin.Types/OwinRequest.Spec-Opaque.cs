// Licensed to Monkey Square, Inc. under one or more contributor 
// license agreements.  See the NOTICE file distributed with 
// this work or additional information regarding copyright 
// ownership.  Monkey Square, Inc. licenses this file to you 
// under the Apache License, Version 2.0 (the "License"); you 
// may not use this file except in compliance with the License.
// You may obtain a copy of the License at 
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.

using System;
using System.Threading.Tasks;
using UpgradeDelegate = System.Action<System.Collections.Generic.IDictionary<string, object>, System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>>;

namespace Owin.Types
{
    public partial struct OwinRequest
    {
        public bool CanUpgrade
        {
            get { return UpgradeDelegate != null; }
        }

        public UpgradeDelegate UpgradeDelegate
        {
            get { return Get<UpgradeDelegate>(OwinConstants.Opaque.Upgrade); }
        }

        public void Upgrade(
            OwinOpaqueParameters parameters,
            Func<OwinOpaque, Task> callback)
        {
            var upgrade = UpgradeDelegate;
            if (upgrade == null)
            {
                throw new NotSupportedException(OwinConstants.Opaque.Upgrade);
            }
            UpgradeDelegate.Invoke(parameters.Dictionary, opaque => callback(new OwinOpaque(opaque)));
        }

        public void Upgrade(
            Func<OwinOpaque, Task> callback)
        {
            Upgrade(OwinOpaqueParameters.Create(), callback);
        }
    }
}
