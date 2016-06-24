using System.Collections.Generic;
using System.Linq;
using AspNetX.Internal;

namespace AspNetX.Initialization
{
    internal class DefaultExtensionProvider<T> : IExtensionProvider<T>
        where T : class
    {
        private readonly ITypeService _typeService;

        public DefaultExtensionProvider(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public IEnumerable<T> Instances
        {
            get { return _typeService.Resolve<T>().ToArray(); }
        }
    }
}
