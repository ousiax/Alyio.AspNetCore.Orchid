using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspNetX.Internal
{
    internal static class TypeActivatorExtensions
    {
        public static T CreateInstance<T>(this ITypeActivator typeActivator, params object[] parameters)
        {
            var activated = typeActivator.CreateInstance(typeof(T), parameters);

            return (T)activated;
        }

        public static IEnumerable<object> CreateInstances(this ITypeActivator typeActivator, IEnumerable<TypeInfo> typeInfos)
        {
            var activated = typeInfos.Select(typeActivator.CreateInstance).Where(o => o != null);

            return activated;
        }

        private static object CreateInstance(this ITypeActivator typeActivator, TypeInfo typeInfo)
        {
            return typeActivator.CreateInstance(typeInfo.AsType());
        }
    }
}
