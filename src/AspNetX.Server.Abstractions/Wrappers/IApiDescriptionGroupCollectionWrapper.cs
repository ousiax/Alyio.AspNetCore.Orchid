using System.Collections.Generic;
using Microsoft.AspNet.Mvc.ApiExplorer;

namespace AspNetX.Server.Abstractions
{
    public interface IApiDescriptionGroupCollectionWrapper
    {
        ApiDescriptionGroupCollection ApiDescriptionGroupCollection { get; }

        IReadOnlyList<IApiDescriptionGroupWrapper> Items { get; }

        int Version { get; }
    }
}
