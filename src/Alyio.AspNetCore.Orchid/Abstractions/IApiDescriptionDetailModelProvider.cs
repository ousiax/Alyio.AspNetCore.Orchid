using System.Collections.Generic;
using Orchid.Models;

namespace Orchid.Abstractions
{
    /// <summary>
    /// Provides access to a cache dictionary of <see cref="ApiDescriptionDetailModel"/>.
    /// </summary>
    public interface IApiDescriptionDetailModelProvider : IReadOnlyDictionary<string, ApiDescriptionDetailModel>
    {
    }
}
