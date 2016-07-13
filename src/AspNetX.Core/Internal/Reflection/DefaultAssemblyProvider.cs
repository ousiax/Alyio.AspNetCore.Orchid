using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace AspNetX.Internal
{
    internal class DefaultAssemblyProvider : IAssemblyProvider
    {
        public IEnumerable<Assembly> GetCandidateAssemblies(string coreLibrary)
        {
            //TODO Remove DefaultAssemblyProvider
            //var libraries = _libraryManager.GetReferencingLibraries(coreLibrary);
            //var assemblyNames = libraries.SelectMany(o => o.Assemblies);
            var assemblyNames = DependencyContext.Default.GetDefaultAssemblyNames();
            var assemblies = assemblyNames.Select(o => Assembly.Load(o));
            return assemblies;
        }
    }
}
