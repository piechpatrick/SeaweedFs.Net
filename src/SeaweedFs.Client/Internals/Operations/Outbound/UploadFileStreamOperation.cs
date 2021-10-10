// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-09-2021
// ***********************************************************************

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using SeaweedFs.Filer.Internals.Operations.Abstractions;
using SeaweedFs.Operations;

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
    internal class UploadFileStreamOperation : OutboundStreamOperation, IFilerOperation<HttpResponseMessage>, IAsyncDisposable
    {
        /// <summary>
        /// The path
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFileStreamOperation"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="stream">The stream.</param>
        public UploadFileStreamOperation(string path, Stream stream)
            : base(stream)
        {
            _path = path;
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
        public Task<HttpResponseMessage> Execute(IFilerClient filerClient)
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
                .WithMultipartStreamFormDataContent(_stream, FileName)
                .Build();
        }
    }
}