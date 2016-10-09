using System.Collections.Generic;
using DoqX.Models;

namespace DoqX.Abstractions
{
    /// <summary>
    /// Provides access to a cache dictionary of <see cref="ApiDescriptionDetailModel"/>.
    /// </summary>
    public interface IApiDescriptionDetailModelProvider : IReadOnlyDictionary<string, ApiDescriptionDetailModel>
    {
    }
}
