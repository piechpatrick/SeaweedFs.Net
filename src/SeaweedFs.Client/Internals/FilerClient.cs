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

namespace SeaweedFs.Filer.Internals
{
    /// <summary>
    /// Class FilerClient. This class cannot be inherited.
    /// Implements the <see cref="IFilerClient" />
    /// </summary>
    /// <seealso cref="IFilerClient" />
    internal sealed class FilerClient : IFilerClient
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilerClient" /> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="options">The options.</param>
        public FilerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Gets the stream asynchronous.
        /// </summary>
        /// <param name="httpRequestMessage">The HTTP request message.</param>
        /// <returns>Task&lt;Content&gt;.</returns>
        Task<Stream> IFilerClient.GetStreamAsync(HttpRequestMessage httpRequestMessage)
        {
            return _httpClient.GetStreamAsync(httpRequestMessage.RequestUri);
        }

        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="httpRequestMessage">The HTTP request message.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> IFilerClient.SendAsync(HttpRequestMessage httpRequestMessage)
        {
            return _httpClient.SendAsync(httpRequestMessage);
        }
    }
}