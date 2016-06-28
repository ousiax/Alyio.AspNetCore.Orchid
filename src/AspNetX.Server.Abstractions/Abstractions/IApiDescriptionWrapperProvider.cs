using System.Collections.Generic;

namespace AspNetX.Server.Abstractions
{
    public interface IApiDescriptionWrapperProvider : IReadOnlyDictionary<string, IApiDescriptionWrapper>
    {
    }
}
