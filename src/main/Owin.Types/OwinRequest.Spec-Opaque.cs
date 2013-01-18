using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Owin.Types
{
    using UpgradeDelegate = Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>>;

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
            IDictionary<string, object> parameters,
            Func<IDictionary<string, object>, Task> callback)
        {
            AcceptDelegate.Invoke(parameters, callback);
        }
    }
}
