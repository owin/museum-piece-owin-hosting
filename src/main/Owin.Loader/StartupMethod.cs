using System;
using Owin.Startup;

namespace Owin.Loader
{
    public class StartupMethod : IStartupMethod
    {
        readonly Action<IAppBuilder> _action;

        public StartupMethod(Action<IAppBuilder> action)
        {
            _action = action;
        }

        public void Invoke(IAppBuilder builder)
        {
            _action.Invoke(builder);
        }
    }
}