using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

[assembly: ImportedFromTypeLib("OWIN")]

namespace Owin
{
    [ComImport]
    [Guid("24d2798e-18c3-461a-97bf-83d5a0fd726e")]
    public interface IAppBuilder
    {
        IDictionary<string, object> Properties { get; }

        IAppBuilder Use(object middleware, params object[] args);

        object Build(Type returnType);

        IAppBuilder New();

        IAppBuilder AddSignatureConversion(Delegate conversion);
    }
}