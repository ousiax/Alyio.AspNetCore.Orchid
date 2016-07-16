using System.Collections.Generic;
using AspNetX.Models;

namespace AspNetX.Abstractions
{
    public interface IApiDescriptionDetailModelProvider : IReadOnlyDictionary<string, ApiDescriptionDetailModel>
    {
    }
}
