// ***********************************************************************
// Assembly         : SeaweedFs.Filer
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SeaweedFs.Filer.Internals.Operations.Abstractions;
using SeaweedFs.Infrastructure.Protocol;
using SeaweedFs.Operations;
using SeaweedFs.Store;

namespace SeaweedFs.Filer.Internals.Operations.Inbound
{
    /// <summary>
    /// Class ListFilesOperation.
    /// Implements the <see cref="SeaweedFs.Operations.OperationBase" />
    /// Implements the <see cref="BlobInfo" />
    /// </summary>
    /// <seealso cref="SeaweedFs.Operations.OperationBase" />
    /// <seealso cref="BlobInfo" />
    internal class ListFilesOperation : OperationBase, IFilerOperation<IEnumerable<BlobInfo>>
    {
        /// <summary>
        /// The path
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListFilesOperation" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="pretty">if set to <c>true</c> [pretty].</param>
        internal ListFilesOperation(string path, bool pretty = true)
        {
            _path = pretty ? path += "?pretty=y" : path;
        }

        /// <summary>
        /// Executes the specified filerClient.
        /// </summary>
        /// <param name="filerClient">The filerClient.</param>
        /// <returns>Task&lt;TResult&gt;.</returns>
        async Task<IEnumerable<BlobInfo>> IFilerOperation<IEnumerable<BlobInfo>>.Execute(IFilerClient filerClient)
        {
            var response = await filerClient.SendAsync(this.BuildRequest());
            if (response.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<DirectoryFileEntriesResponse>(await response.Content.ReadAsStringAsync())
                           ?.Entries.Where(ex => Convert.ToString(ex.Mode, 8) == "660")
                           .Select(e => new BlobInfo(Path.GetFileName(e.FullPath)))
                       ?? new List<BlobInfo>();
            return new List<BlobInfo>();
        }
        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>HttpRequestMessage.</returns>
        protected virtual HttpRequestMessage BuildRequest()
        {
            return HttpRequestBuilder.WithRelativeUrl(_path)
                .WithMethod(HttpMethod.Get)
                .Build();
        }
    }
}