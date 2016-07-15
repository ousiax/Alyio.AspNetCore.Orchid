using System;
using System.Collections.Generic;
using System.Reflection;

namespace AspNetX.Internal
{
    internal class DefaultTypeService : ITypeService
    {
        private readonly ITypeActivator _typesActivator;
        private readonly ITypeSelector _typeSelector;
        private readonly IAssemblyPartDiscoveryProvider _assemblyProvider;

        public DefaultTypeService(ITypeActivator typesActivator, ITypeSelector typeSelector, IAssemblyPartDiscoveryProvider discoveryProvider)
        {
            _typesActivator = typesActivator;
            _typeSelector = typeSelector;
            _assemblyProvider = discoveryProvider;
        }

        public IEnumerable<object> Resolve(Type targetType)
        {
            var types = ResolveTypes(targetType);
            var instances = _typesActivator.CreateInstances(types);

            return instances;
        }

        public IEnumerable<TypeInfo> ResolveTypes(Type targetType)
        {
            var assemblies = _assemblyProvider.GetCandidateAssemblies();
            var types = _typeSelector.FindTypes(assemblies, targetType.GetTypeInfo());

            return types;
        }
    }
}
