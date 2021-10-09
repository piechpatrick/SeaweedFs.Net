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
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Client.Operations.Inbound
{
    /// <summary>
    /// Class GetFileStreamOperation.
    /// Implements the <see cref="SeaweedFs.Client.Operations.Abstractions.OperationBase" />
    /// Implements the <see cref="SeaweedFs.Client.Operations.Abstractions.IFilerOperation{System.IO.Stream}" />
    /// </summary>
    /// <seealso cref="SeaweedFs.Client.Operations.Abstractions.OperationBase" />
    /// <seealso cref="SeaweedFs.Client.Operations.Abstractions.IFilerOperation{System.IO.Stream}" />
    internal class GetFileStreamOperation : OperationBase, IFilerOperation<Stream>
    {
        /// <summary>
        /// The path
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFileStreamOperation"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public GetFileStreamOperation(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Executes the specified filerService.
        /// </summary>
        /// <param name="filerService">The filerService.</param>
        /// <returns>Task&lt;TResult&gt;.</returns>
        public Task<Stream> Execute(IFilerService filerService)
        {
            return filerService.GetStreamAsync(HttpRequestBuilder
                .WithMethod(HttpMethod.Get)
                .WithRelativeUrl(_path)
                .Build());
        }
    }
}