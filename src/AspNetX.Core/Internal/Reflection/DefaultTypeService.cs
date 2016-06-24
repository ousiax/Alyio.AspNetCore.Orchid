using System;
using System.Collections.Generic;
using System.Reflection;

namespace AspNetX.Internal
{
    internal class DefaultTypeService : ITypeService
    {
        private readonly ITypeActivator _typesActivator;
        private readonly ITypeSelector _typeSelector;
        private readonly IAssemblyProvider _assemblyProvider;

        public DefaultTypeService(ITypeActivator typesActivator, ITypeSelector typeSelector, IAssemblyProvider assemblyProvider)
        {
            _typesActivator = typesActivator;
            _typeSelector = typeSelector;
            _assemblyProvider = assemblyProvider;
        }

        public IEnumerable<object> Resolve(string coreLibrary, Type targetType)
        {
            var types = ResolveTypes(coreLibrary, targetType);
            var instances = _typesActivator.CreateInstances(types);

            return instances;
        }

        public IEnumerable<object> Resolve(IEnumerable<Assembly> assemblies, Type targetType)
        {
            var types = ResolveTypes(assemblies, targetType);
            var instances = _typesActivator.CreateInstances(types);

            return instances;
        }

        public IEnumerable<TypeInfo> ResolveTypes(string coreLibrary, Type targetType)
        {
            var assemblies = _assemblyProvider.GetCandidateAssemblies(coreLibrary);

            return ResolveTypes(assemblies, targetType);
        }

        public IEnumerable<TypeInfo> ResolveTypes(IEnumerable<Assembly> assemblies, Type targetType)
        {
            var types = _typeSelector.FindTypes(assemblies, targetType.GetTypeInfo());

            return types;
        }
    }
}
