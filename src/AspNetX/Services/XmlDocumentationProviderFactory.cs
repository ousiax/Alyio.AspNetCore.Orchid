using System;
using System.IO;
using System.Reflection;
using AspNetX.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetX.Services
{
    /// <inheritdoc />
    public class XmlDocumentationProviderFactory : IDocumentationProviderFactory
    {
        private IDocumentationProvider _documentationProvider;

        private readonly ServerOptions _serverOptions;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;

        /// <summary>
        /// Creates a new instance of <see cref="XmlDocumentationProviderFactory"/>.
        /// </summary>
        /// <param name="serverOptions">
        /// The <see cref="IOptions{ServerOptions}"/>.
        /// </param>
        /// <param name="hostingEnvironment">
        /// The <see cref="IHostingEnvironment"/>.
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/>.
        /// </param>
        public XmlDocumentationProviderFactory(IOptions<ServerOptions> serverOptions, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            _serverOptions = serverOptions.Value;
            _hostingEnvironment = hostingEnvironment;
            _logger = loggerFactory.CreateLogger<XmlDocumentationProviderFactory>();
        }

        /// <inheritdoc />
        public IDocumentationProvider Create()
        {
            if (_documentationProvider == null)
            {
                var documentationPath = GetDocumentationPath();
                if (!string.IsNullOrWhiteSpace(documentationPath) && File.Exists(documentationPath))
                {
                    try
                    {
                        _documentationProvider = new XmlDocumentationProvider(documentationPath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning("Couldn't create documentation. Please specify a valid documentation path.", ex);
                    }
                }
            }
            return _documentationProvider;
        }

        private string GetDocumentationPath()
        {
            string[] lookupPaths = new[] {
                _hostingEnvironment.ContentRootPath,
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
            };

            var documentationPath = _serverOptions.DocumentationPath;

            if (string.IsNullOrWhiteSpace(documentationPath) || !File.Exists(documentationPath))
            {
                _logger.LogWarning($"Documentation path not be specified or not a valid file path.");

                var fileName = $"{Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().GetName().Name)}.xml";
                foreach (var basePath in lookupPaths)
                {
                    documentationPath = Path.Combine(basePath, fileName);
                    if (!File.Exists(documentationPath))
                    {
                        _logger.LogWarning($"[Failure]: Try to look up the documentation at [{documentationPath}].");
                    }
                    else
                    {
                        _logger.LogWarning($"[Success]: Try to look up the documentation at [{documentationPath}].");
                    }
                }
            }

            return documentationPath;
        }
    }
}
