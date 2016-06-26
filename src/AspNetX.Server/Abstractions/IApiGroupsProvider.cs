using AspNetX.Server.Models;

namespace AspNetX.Server.Abstractions
{
    public interface IApiGroupsProvider
    {
        ApiXDescriptionGroupCollection ApiXDescriptionGroups { get; }
    }
}
