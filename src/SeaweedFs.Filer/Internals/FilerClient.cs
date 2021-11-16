// ***********************************************************************
// Assembly         : SeaweedFs.Filer
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-13-2021
// ***********************************************************************

using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SeaweedFs.Filer.Internals
{
    /// <summary>
    ///     Class FilerClient. This class cannot be inherited.
    ///     Implements the <see cref="IFilerClient" />
    /// </summary>
    /// <seealso cref="IFilerClient" />
    internal sealed class FilerClient : IFilerClient
    {
        /// <summary>
        ///     The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FilerClient" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public FilerClient(HttpClient httpClient) => _httpClient = httpClient;

        /// <summary>
        ///     Gets the stream asynchronous.
        /// </summary>
        /// <param name="httpRequestMessage">The HTTP request message.</param>
        /// <returns>Task&lt;Content&gt;.</returns>
        Task<Stream> IFilerClient.GetStreamAsync(HttpRequestMessage httpRequestMessage) =>
            _httpClient.GetStreamAsync(httpRequestMessage.RequestUri);

        /// <summary>
        ///     Sends the asynchronous.
        /// </summary>
        /// <param name="httpRequestMessage">The HTTP request message.</param>
        /// <param name="httpCompletionOption">The HTTP completion option.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> IFilerClient.SendAsync(HttpRequestMessage httpRequestMessage,
            HttpCompletionOption httpCompletionOption, CancellationToken cancellationToken = default) =>
            _httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken);
    }
}