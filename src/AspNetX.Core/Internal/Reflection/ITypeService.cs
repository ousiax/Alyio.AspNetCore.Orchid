using System;
using System.Collections.Generic;
using System.Reflection;

namespace AspNetX.Internal
{
    internal interface ITypeService
    {
        IEnumerable<object> Resolve(string coreLibrary, Type targetType);

        IEnumerable<object> Resolve(IEnumerable<Assembly> assemblies, Type targetType);

        IEnumerable<TypeInfo> ResolveTypes(string coreLibrary, Type targetType);

        IEnumerable<TypeInfo> ResolveTypes(IEnumerable<Assembly> assemblies, Type targetType);
    }
}
