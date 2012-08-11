using System;

namespace Owin.Loader
{
    public interface IStartupLoader
    {
        Action<IAppBuilder> Load(string startup);
    }
}
