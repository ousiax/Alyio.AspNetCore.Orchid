using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;

namespace AspNetX.Internal
{
    internal class DefaultAssemblyProvider : IAssemblyProvider
    {
        private readonly ILibraryManager _libraryManager;

        public DefaultAssemblyProvider(ILibraryManager libraryManager)
        {
            this._libraryManager = libraryManager;
        }

        public IEnumerable<Assembly> GetCandidateAssemblies(string coreLibrary)
        {
            var libraries = _libraryManager.GetReferencingLibraries(coreLibrary);
            var assemblyNames = libraries.SelectMany(o => o.Assemblies);
            var assemblies = assemblyNames.Select(o => Assembly.Load(o));
            return assemblies;
        }
    }
}
