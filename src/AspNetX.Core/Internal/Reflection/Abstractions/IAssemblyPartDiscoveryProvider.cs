using System.Collections.Generic;
using System.Reflection;

namespace AspNetX.Internal
{
    /// <summary>
    /// Discovers assemblies that are part of the AspNetX component using the DependencyContext.
    /// </summary>
    internal interface IAssemblyPartDiscoveryProvider
    {
        IReadOnlyList<Assembly> GetCandidateAssemblies();
    }
}
