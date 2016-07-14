using System;
using System.Collections.Generic;
using System.Reflection;

namespace AspNetX.Internal
{
    public interface ITypeService
    {
        IEnumerable<object> Resolve(Type targetType);

        IEnumerable<TypeInfo> ResolveTypes(Type targetType);
    }
}
