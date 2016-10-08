using System.Collections.Generic;
using DocX.Models;

namespace DocX.Abstractions
{
    /// <summary>
    /// Provides access to a cache dictionary of <see cref="ApiDescriptionDetailModel"/>.
    /// </summary>
    public interface IApiDescriptionDetailModelProvider : IReadOnlyDictionary<string, ApiDescriptionDetailModel>
    {
    }
}
