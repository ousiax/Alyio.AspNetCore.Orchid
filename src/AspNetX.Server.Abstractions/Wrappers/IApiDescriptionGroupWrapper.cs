using System.Collections.Generic;
using Microsoft.AspNet.Mvc.ApiExplorer;

namespace AspNetX.Server.Abstractions
{
    public interface IApiDescriptionGroupWrapper
    {
        ApiDescriptionGroup ApiDescriptionGroup { get; }

        string GroupName { get; }

        IReadOnlyList<IApiDescriptionWrapper> Items { get; }
    }
}
