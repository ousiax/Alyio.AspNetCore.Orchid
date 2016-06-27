using AspNetX.Server.Models;

namespace AspNetX.Server.Abstractions
{
    public interface IApiDescriptionGroupCollectionWrapperProvider
    {
        ApiDescriptionGroupCollectionWrapper ApiXDescriptionGroups { get; }
    }
}
