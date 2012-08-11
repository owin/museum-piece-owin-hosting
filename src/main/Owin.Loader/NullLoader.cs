using Owin.Startup;

namespace Owin.Loader
{
    public class NullLoader : IStartupLoader
    {
        static readonly IStartupLoader Singleton = new NullLoader();

        public static IStartupLoader Instance { get { return Singleton; } }

        public IStartupMethod Load(string startup)
        {
            return null;
        }
    }
}
