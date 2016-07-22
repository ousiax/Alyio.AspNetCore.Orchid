using System.Collections.Generic;
using AspNetX.Models;

namespace AspNetX.Abstractions
{
    /// <summary>
    /// Provides access to a cache dictionary of <see cref="ApiDescriptionDetailModel"/>.
    /// </summary>
    public interface IApiDescriptionDetailModelProvider : IReadOnlyDictionary<string, ApiDescriptionDetailModel>
    {
    }
}
