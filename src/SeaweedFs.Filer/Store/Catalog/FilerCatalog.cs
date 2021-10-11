// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using SeaweedFs.Filer.Internals.Operations;
using SeaweedFs.Filer.Internals.Operations.Inbound;
using SeaweedFs.Filer.Internals.Operations.Outbound;
using SeaweedFs.Store;
using System.Collections.Generic;
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
            Directory = Path.GetDirectoryName(directory);
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
        /// <param name="blob">The BLOB.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        public async Task<bool> PushAsync(Blob blob)
        {
            await using var operation = new UploadFileStreamOperation(Path.Combine(Directory, blob.BlobInfo.Name), blob.BlobInfo, blob.Content);
            return await _executor.Execute(operation);
        }
        /// <summary>
        /// Gets the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Blob.</returns>
        public async Task<Blob> GetAsync(string fileName)
        {
            var operation = new GetFileStreamOperation(Path.Combine(Directory, Path.GetFileName(fileName)));
            return new Blob(fileName, await _executor.Execute(operation));
        }
        /// <summary>
        /// Gets the specified BLOB information.
        /// </summary>
        /// <param name="blobInfo">The BLOB information.</param>
        /// <returns>Blob.</returns>
        public Task<Blob> GetAsync(BlobInfo blobInfo)
        {
            return this.GetAsync(blobInfo.Name);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="blobInfo">The BLOB information.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> DeleteAsync(BlobInfo blobInfo)
        {
            var operation = new DeleteOperation(Path.Combine(Directory, blobInfo.Name));
            return _executor.Execute(operation);
        }

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;BlobInfo&gt;&gt;.</returns>
        public Task<IEnumerable<BlobInfo>> ListAsync()
        {
            var operation = new ListFilesOperation(Directory);
            return _executor.Execute(operation);
        }
    }
}