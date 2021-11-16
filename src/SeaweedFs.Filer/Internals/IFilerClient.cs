// ***********************************************************************
// Assembly         : SeaweedFs.Filer
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-13-2021
// ***********************************************************************

using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SeaweedFs.Abstractions;

namespace SeaweedFs.Filer.Internals
{
    /// <summary>
    ///     Interface IFilerClient
    ///     Implements the <see cref="SeaweedFs.Abstractions.IOperator" />
    /// </summary>
    /// <seealso cref="SeaweedFs.Abstractions.IOperator" />
    internal interface IFilerClient : IOperator
    {
        /// <summary>
        ///     Sends the asynchronous.
        /// </summary>
        /// <param name="httpRequestMessage">The HTTP request message.</param>
        /// <param name="httpCompletionOption">The HTTP completion option.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage,
            HttpCompletionOption httpCompletionOption, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets the stream asynchronous.
        /// </summary>
        /// <param name="httpRequestMessage">The HTTP request message.</param>
        /// <returns>Task&lt;Content&gt;.</returns>
        Task<Stream> GetStreamAsync(HttpRequestMessage httpRequestMessage);
    }
}