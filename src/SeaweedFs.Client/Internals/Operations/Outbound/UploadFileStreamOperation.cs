// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using SeaweedFs.Filer.Internals.Operations.Abstractions;
using SeaweedFs.Operations;
using SeaweedFs.Store;

namespace SeaweedFs.Filer.Internals.Operations.Outbound
{
    /// <summary>
    /// Class UploadFileStreamOperation.
    /// Implements the <see cref="OutboundStreamOperation" />
    /// Implements the <see cref="HttpResponseMessage" />
    /// Implements the <see cref="System.IAsyncDisposable" />
    /// </summary>
    /// <seealso cref="OutboundStreamOperation" />
    /// <seealso cref="HttpResponseMessage" />
    /// <seealso cref="System.IAsyncDisposable" />
    internal class UploadFileStreamOperation : OutboundStreamOperation, IFilerOperation<HttpResponseMessage>
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
        /// Initializes a new instance of the <see cref="UploadFileStreamOperation" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="blobInfo">The BLOB information.</param>
        /// <param name="stream">The stream.</param>
        public UploadFileStreamOperation(string path,BlobInfo blobInfo, Stream stream)
            : base(stream)
        {
            _path = path;
            _blobInfo = blobInfo;
        }
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName => Path.GetFileName(_path);
        /// <summary>
        /// Executes the specified filerClient.
        /// </summary>
        /// <param name="filerClient">The filerClient.</param>
        /// <returns>Task&lt;TResult&gt;.</returns>
        Task<HttpResponseMessage> IFilerOperation<HttpResponseMessage>.Execute(IFilerClient filerClient)
        {
            var request = this.BuildRequest();
            return filerClient.SendAsync(request);
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>HttpRequestMessage.</returns>
        protected virtual HttpRequestMessage BuildRequest()
        {
            return HttpRequestBuilder.WithRelativeUrl(_path)
                .WithMethod(HttpMethod.Post)
                .WithHeaders(_blobInfo.Headers)
                .WithMultipartStreamFormDataContent(_stream, FileName)
                .Build();
        }
    }
}