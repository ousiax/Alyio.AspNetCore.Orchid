using DocX.Models;

namespace DocX.Abstractions
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
