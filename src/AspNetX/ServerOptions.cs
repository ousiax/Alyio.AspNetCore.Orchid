namespace AspNetX
{
    /// <summary>
    /// Provides programmatic configuration for the AspNetX framework.
    /// </summary>
    public class ServerOptions
    {
        /// <summary>
        /// Gets or sets the base path of AspNetX server. Default is api-docs.
        /// </summary>
        /// <example>http://localhost:8000/{api-docs}</example>
        public string BasePath { get; set; } = "api-docs";

        /// <summary>
        /// Gets or sets the documentation path of your app.
        /// </summary>
        public string DocumentationPath { get; set; }

        /// <summary>
        /// Gets or sets a general description of the api in you app.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the about file path. (Styling with Markdown is supported)
        /// </summary>
        public string About { get; set; } = "about.md";
    }
}
