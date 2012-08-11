using System;

namespace Owin.Loader
{
    public class NullLoader : IStartupLoader
    {
        static readonly IStartupLoader Singleton = new NullLoader();

        public static IStartupLoader Instance { get { return Singleton; } }

        public Action<IAppBuilder> Load(string startup)
        {
            return null;
        }
    }
}
