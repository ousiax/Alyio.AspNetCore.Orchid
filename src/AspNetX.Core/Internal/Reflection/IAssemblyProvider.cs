using System.Collections.Generic;
using System.Reflection;

namespace  AspNetX.Internal
{
    internal interface IAssemblyProvider
    {
        IEnumerable<Assembly> GetCandidateAssemblies(string coreLibrary);
    }
}
