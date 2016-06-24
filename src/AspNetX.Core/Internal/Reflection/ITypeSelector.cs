using System.Collections.Generic;
using System.Reflection;

namespace AspNetX.Internal
{
    internal interface ITypeSelector
    {
        IEnumerable<TypeInfo> FindTypes(IEnumerable<Assembly> targetAssmblies, TypeInfo targetTypeInfo);
    }
}
