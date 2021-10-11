// ***********************************************************************
// Assembly         : SeaweedFs.Filer
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using SeaweedFs.Filer.Internals.Operations.Abstractions;
using SeaweedFs.Operations;

namespace SeaweedFs.Filer.Internals.Operations.Inbound
{
    /// <summary>
    /// Class GetFileStreamOperation.
    /// Implements the <see cref="OperationBase" />
    /// Implements the <see cref="Stream" />
    /// </summary>
    /// <seealso cref="OperationBase" />
    /// <seealso cref="Stream" />
    internal class GetFileStreamOperation : OperationBase, IFilerOperation<Stream>
    {
        /// <summary>
        /// The path
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFileStreamOperation" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public GetFileStreamOperation(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Executes the specified filerClient.
        /// </summary>
        /// <param name="filerClient">The filerClient.</param>
        /// <returns>Task&lt;TResult&gt;.</returns>
        public Task<Stream> Execute(IFilerClient filerClient)
        {
            return filerClient.GetStreamAsync(HttpRequestBuilder
                .WithMethod(HttpMethod.Get)
                .WithRelativeUrl(_path)
                .Build());
        }
    }
}