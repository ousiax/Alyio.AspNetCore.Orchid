using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AspNetX.Server.Abstractions
{
    public interface IApiDescriptionGroupCollectionWrapper
    {
        ApiDescriptionGroupCollection ApiDescriptionGroupCollection { get; }

        IReadOnlyList<IApiDescriptionGroupWrapper> Items { get; }

        int Version { get; }
    }
}
