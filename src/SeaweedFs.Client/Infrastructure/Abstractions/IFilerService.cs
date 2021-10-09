// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-09-2021
// ***********************************************************************
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Client.Infrastructure.Abstractions
{
    /// <summary>
    /// Interface IFilerService
    /// Implements the <see cref="SeaweedFs.Client.Infrastructure.Abstractions.IOperator" />
    /// </summary>
    /// <seealso cref="SeaweedFs.Client.Infrastructure.Abstractions.IOperator" />
    internal interface IFilerService : IOperator
    {
        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="httpRequestMessage">The HTTP request message.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage);
        /// <summary>
        /// Gets the stream asynchronous.
        /// </summary>
        /// <param name="httpRequestMessage">The HTTP request message.</param>
        /// <returns>Task&lt;Stream&gt;.</returns>
        Task<Stream> GetStreamAsync(HttpRequestMessage httpRequestMessage);
    }
}