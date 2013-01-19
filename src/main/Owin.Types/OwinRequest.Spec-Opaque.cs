using System;
using System.Collections.Generic;
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
