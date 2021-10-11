// ***********************************************************************
// Assembly         : SeaweedFs.Filer
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using System;
using SeaweedFs.Filer.Internals.Operations;
using SeaweedFs.Filer.Internals.Operations.Inbound;
using SeaweedFs.Filer.Internals.Operations.Outbound;
using SeaweedFs.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// <param name="progress">The progress.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        public async Task<bool> PushAsync(Blob blob, IProgress<int> progress = null)
        {
            await using var operation = new UploadFileOutboundStreamOperation(Path.Combine(Directory, blob.BlobInfo.Name), blob.BlobInfo, blob.Content, progress);
            return await _executor.Execute(operation);
        }
        /// <summary>
        /// Gets the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="progress">The progress.</param>
        /// <returns>Blob.</returns>
        public async Task<Blob> GetAsync(string fileName, IProgress<int> progress = null)
        {
            var operation = new GetFileStreamOperation(Path.Combine(Directory, Path.GetFileName(fileName)), progress);
            var response = await _executor.Execute(operation);
            var blobInfo = new BlobInfo(fileName);
            foreach (var header in response.Item1.Headers)
                blobInfo.Headers.Add(header.Key, header.Value);
            return new Blob(blobInfo, response.Item2);
        }
        /// <summary>
        /// Gets the specified BLOB information.
        /// </summary>
        /// <param name="blobInfo">The BLOB information.</param>
        /// <param name="progress">The progress.</param>
        /// <returns>Blob.</returns>
        public Task<Blob> GetAsync(BlobInfo blobInfo, IProgress<int> progress = null)
        {
            return this.GetAsync(blobInfo.Name, progress);
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