// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-09-2021
// ***********************************************************************
using SeaweedFs.Client.Infrastructure.Abstractions;
using SeaweedFs.Client.Operations.Abstractions;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Client.Operations.Outbound
{
    /// <summary>
    /// Class UploadFileStreamOperation.
    /// Implements the <see cref="SeaweedFs.Client.Operations.Abstractions.OutboundStreamOperation" />
    /// Implements the <see cref="SeaweedFs.Client.Operations.Abstractions.IFilerOperation{System.Net.Http.HttpResponseMessage}" />
    /// Implements the <see cref="System.IAsyncDisposable" />
    /// </summary>
    /// <seealso cref="SeaweedFs.Client.Operations.Abstractions.OutboundStreamOperation" />
    /// <seealso cref="SeaweedFs.Client.Operations.Abstractions.IFilerOperation{System.Net.Http.HttpResponseMessage}" />
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
        /// Executes the specified filerService.
        /// </summary>
        /// <param name="filerService">The filerService.</param>
        /// <returns>Task&lt;TResult&gt;.</returns>
        public Task<HttpResponseMessage> Execute(IFilerService filerService)
        {
            var request = this.BuildRequest();
            return filerService.SendAsync(request);
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