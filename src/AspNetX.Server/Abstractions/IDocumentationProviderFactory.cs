namespace AspNetX.Abstractions
{
    /// <summary>
    /// Create an instance of type <see cref="IDocumentationProvider"/>.
    /// </summary>
    public interface IDocumentationProviderFactory
    {
        /// <summary>
        /// Create an instance of type <see cref="IDocumentationProvider"/>.
        /// </summary>
        IDocumentationProvider Create();
    }
}
