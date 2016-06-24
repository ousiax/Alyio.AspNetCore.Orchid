using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspNetX.Internal
{
    internal static class TypeServiceExtensions
    {
        private static readonly string _defaultLibrary = typeof(TypeServiceExtensions).GetTypeInfo().Assembly.GetName().Name;

        public static IEnumerable<object> Resolve(this ITypeService typeService, Type targetType)
        {
            return typeService.Resolve(_defaultLibrary, targetType);
        }

        public static IEnumerable<T> Resolve<T>(this ITypeService typeService)
        {
            return typeService.Resolve(typeof(T)).Select(o => (T)o);
        }

        public static IEnumerable<T> Resolve<T>(this ITypeService typeService, string coreLibrary)
        {
            return typeService.Resolve<T>(coreLibrary);
        }

        public static IEnumerable<T> Resolve<T>(this ITypeService typeService, IEnumerable<Assembly> assemblies)
        {
            return typeService.Resolve(assemblies, typeof(T)).Select(o => (T)o);
        }

        public static IEnumerable<TypeInfo> ResolveTypes<T>(this ITypeService typeService, string coreLibrary)
        {
            return typeService.ResolveTypes(coreLibrary, typeof(T));
        }

        public static IEnumerable<TypeInfo> ResolveTypes<T>(this ITypeService typeService, IEnumerable<Assembly> assemblies)
        {
            return typeService.ResolveTypes(assemblies, typeof(T));
        }
    }
}
