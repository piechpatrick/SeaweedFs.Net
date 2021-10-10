// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-10-2021
// ***********************************************************************

using SeaweedFs.Filer.Internals.Operations;
using SeaweedFs.Filer.Internals.Operations.Outbound;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Filer.Store.Catalog
{
    /// <summary>
    /// Class FilerCatalog. This class cannot be inherited.
    /// Implements the <see cref="SeaweedFs.Filer.Store.Catalog.IFilerCatalog" />
    /// </summary>
    /// <seealso cref="SeaweedFs.Filer.Store.Catalog.IFilerCatalog" />
    internal sealed class FilerCatalog : IFilerCatalog
    {
        /// <summary>
        /// The filer store
        /// </summary>
        private readonly IFilerStore _filerStore;
        /// <summary>
        /// The executor
        /// </summary>
        private readonly IFilerOperationsExecutor _executor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilerCatalog" /> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="filerStore">The filer store.</param>
        /// <param name="executor">The executor.</param>
        internal FilerCatalog(string directory, IFilerStore filerStore, IFilerOperationsExecutor executor)
        {
            Directory = directory;
            _filerStore = filerStore;
            _executor = executor;
        }
        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <value>The directory.</value>
        public string Directory { get; }

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="stream">The stream.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        public async Task<HttpResponseMessage> Upload(string fileName, Stream stream)
        {
            await using var operation = new UploadFileStreamOperation(Path.Combine(Directory, fileName), stream);
            return await _executor.Execute(operation);
        }
    }
}