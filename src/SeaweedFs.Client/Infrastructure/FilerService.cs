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
using SeaweedFs.Filer.Infrastructure.Abstractions;

namespace SeaweedFs.Filer.Infrastructure
{
    /// <summary>
    /// Class FilerServiceService. This class cannot be inherited.
    /// Implements the <see cref="IFilerService" />
    /// </summary>
    /// <seealso cref="IFilerService" />
    internal sealed class FilerService : IFilerService
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilerServiceService" /> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="options">The options.</param>
        public FilerService(IHttpClientFactory httpClientFactory, SeaweedOptions options)
        {
            _httpClient = httpClientFactory.CreateClient(options.FilerHttpClientName);
        }

        /// <summary>
        /// Gets the stream asynchronous.
        /// </summary>
        /// <param name="httpRequestMessage">The HTTP request message.</param>
        /// <returns>Task&lt;Stream&gt;.</returns>
        Task<Stream> IFilerService.GetStreamAsync(HttpRequestMessage httpRequestMessage)
        {
            return _httpClient.GetStreamAsync(httpRequestMessage.RequestUri);
        }

        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="httpRequestMessage">The HTTP request message.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> IFilerService.SendAsync(HttpRequestMessage httpRequestMessage)
        {
            return _httpClient.SendAsync(httpRequestMessage);
        }
    }
}