using System;

namespace AspNetX.Internal
{
    internal interface ITypeActivator
    {
        object CreateInstance(Type instanceType, params object[] parameters);
    }
}
