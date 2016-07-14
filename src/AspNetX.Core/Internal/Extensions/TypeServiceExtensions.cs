using System.Collections.Generic;
using System.Linq;

namespace AspNetX.Internal
{
    internal static class TypeServiceExtensions
    {
        public static IEnumerable<T> Resolve<T>(this ITypeService typeService)
        {
            return typeService.Resolve(typeof(T)).Select(o => (T)o);
        }
    }
}
