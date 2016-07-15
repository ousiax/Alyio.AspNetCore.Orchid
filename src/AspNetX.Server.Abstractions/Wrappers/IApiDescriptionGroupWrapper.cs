using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AspNetX.Server.Abstractions
{
    public interface IApiDescriptionGroupWrapper
    {
        ApiDescriptionGroup ApiDescriptionGroup { get; }

        string GroupName { get; }

        IReadOnlyList<IApiDescriptionWrapper> Items { get; }
    }
}
