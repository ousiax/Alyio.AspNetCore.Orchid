using Alyio.AspNetCore.Orchid.Models;

namespace Alyio.AspNetCore.Orchid.Abstractions
{
    /// <summary>
    /// Provides access to a collection of <see cref="ApiDescriptionGroupModel"/>.
    /// </summary>
    public interface IApiDescriptionGroupModelCollectionProvider
    {
        /// <summary>
        /// Gets a collection of <see cref="ApiDescriptionGroupModel"/>.
        /// </summary>
        ApiDescriptionGroupModelCollection ApiDescriptionGroups { get; }
    }
}
