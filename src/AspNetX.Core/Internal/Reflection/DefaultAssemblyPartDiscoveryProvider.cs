using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace AspNetX.Internal
{
    internal sealed class DefaultAssemblyPartDiscoveryProvider : IAssemblyPartDiscoveryProvider
    {
        private IReadOnlyList<Assembly> _candidateAssemblies;

        private readonly string _referenceAssembly;

        public DefaultAssemblyPartDiscoveryProvider()
        {
            _referenceAssembly = this.GetType().GetTypeInfo().Assembly.GetName().Name;
        }

        public IReadOnlyList<Assembly> GetCandidateAssemblies()
        {
            if (_candidateAssemblies == null)
            {
                Assembly entryAssembly = Assembly.GetEntryAssembly();
                DependencyContext dependencyContext = DependencyContext.Load(Assembly.GetEntryAssembly());
                if (dependencyContext == null)
                {
                    _candidateAssemblies = new[] { entryAssembly };
                }
                else
                {
                    _candidateAssemblies = GetCandidateLibraries(dependencyContext)
                    .SelectMany(libaray => libaray.GetDefaultAssemblyNames(dependencyContext))
                    .Select(Assembly.Load)
                    .ToList()
                    .AsReadOnly();
                }
            }
            return _candidateAssemblies;
        }

        // Returns a list of libraries that references the assemblies in <see cref="ReferenceAssemblies"/>.
        private IEnumerable<RuntimeLibrary> GetCandidateLibraries(DependencyContext dependencyContext)
        {
            var runtimeLibraries = dependencyContext.RuntimeLibraries.ToDictionary(d => d.Name, d => d);
            foreach (var library in dependencyContext.RuntimeLibraries)
            {
                if (IsCandidate(library, runtimeLibraries))
                {
                    yield return library;
                }
            }
        }

        private bool IsCandidate(RuntimeLibrary library, IDictionary<string, RuntimeLibrary> runtimeLibraries)
        {
            return _referenceAssembly.Equals(library.Name, StringComparison.OrdinalIgnoreCase)
                || library.Dependencies.Any(d => IsCandidate(runtimeLibraries[d.Name], runtimeLibraries));
        }
    }
}
