using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspNetX.Internal
{
    internal class DefaultTypeSelector : ITypeSelector
    {
        public IEnumerable<TypeInfo> FindTypes(IEnumerable<Assembly> targetAssmblies, TypeInfo targetTypeInfo)
        {
            var typeInfos = targetAssmblies.SelectMany(o => o.DefinedTypes)
                            .Where(t => t.IsClass
                            && !t.IsAbstract
                            && !t.ContainsGenericParameters
                            && targetTypeInfo.IsAssignableFrom(t));
            return typeInfos;
        }
    }
}
