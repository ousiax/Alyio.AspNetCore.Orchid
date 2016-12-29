using System.Collections.Generic;
using Alyio.AspNetCore.Orchid.Models;

namespace Alyio.AspNetCore.Orchid.Abstractions
{
    /// <summary>
    /// Provides access to a cache dictionary of <see cref="ApiDescriptionDetailModel"/>.
    /// </summary>
    public interface IApiDescriptionDetailModelProvider : IReadOnlyDictionary<string, ApiDescriptionDetailModel>
    {
    }
}
