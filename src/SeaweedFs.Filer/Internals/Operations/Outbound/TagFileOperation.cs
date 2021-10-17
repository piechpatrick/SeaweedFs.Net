// ***********************************************************************
// Assembly         : SeaweedFs.Filer
// Author           : piechpatrick
// Created          : 10-11-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************
using SeaweedFs.Filer.Internals.Operations.Abstractions;
using SeaweedFs.Operations;
using SeaweedFs.Store;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Filer.Internals.Operations.Outbound
{
    /// <summary>
    /// Class TagFileOperation.
    /// Implements the <see cref="SeaweedFs.Operations.OperationBase" />
    /// Implements the <see cref="SeaweedFs.Filer.Internals.Operations.Abstractions.IFilerOperation{System.Boolean}" />
    /// </summary>
    /// <seealso cref="SeaweedFs.Operations.OperationBase" />
    /// <seealso cref="SeaweedFs.Filer.Internals.Operations.Abstractions.IFilerOperation{System.Boolean}" />
    internal class TagFileOperation : OperationBase, IFilerOperation<bool>
    {
        /// <summary>
        /// The path
        /// </summary>
        private readonly string _path;
        /// <summary>
        /// The BLOB information
        /// </summary>
        private readonly BlobInfo _blobInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagFileOperation" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="blobInfo">The BLOB information.</param>
        public TagFileOperation(string path, BlobInfo blobInfo)
        {
            _path = path;
            _blobInfo = blobInfo;
        }
        /// <summary>
        /// Executes the specified filerClient.
        /// </summary>
        /// <param name="filerClient">The filerClient.</param>
        /// <returns>Task&lt;TResult&gt;.</returns>
        async Task<bool> IFilerOperation<bool>.Execute(IFilerClient filerClient)
        {
            var response = await filerClient.SendAsync(this.BuildRequest(), HttpCompletionOption.ResponseContentRead);
            return response.IsSuccessStatusCode;
        }
        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>HttpRequestMessage.</returns>
        protected virtual HttpRequestMessage BuildRequest()
        {
            return HttpRequestBuilder.WithRelativeUrl(_path + "?tagging")
                .WithMethod(HttpMethod.Put)
                .WithHeaders(_blobInfo.Headers)
                .Build();
        }
    }
}