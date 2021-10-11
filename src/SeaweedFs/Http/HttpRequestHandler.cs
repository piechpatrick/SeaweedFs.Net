// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Http
{
    /// <summary>
    /// Class HttpRequestHandler.
    /// Implements the <see cref="SeaweedFs.Infrastructure.Http.IHttpRequestHandler`1" />
    /// </summary>
    /// <typeparam name="TRequestBuilder">The type of the t request builder.</typeparam>
    /// <seealso cref="SeaweedFs.Infrastructure.Http.IHttpRequestHandler`1" />
    internal class HttpRequestHandler<TRequestBuilder> : IHttpRequestHandler<TRequestBuilder> where TRequestBuilder : IHttpRequestBuilder
    {

        /// <summary>
        /// The HTTP client
        /// </summary>
        protected readonly HttpClient _httpClient;
        /// <summary>
        /// The request builder
        /// </summary>
        private readonly TRequestBuilder _requestBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestHandler{TRequestBuilder}" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="requestBuilder">The request builder.</param>
        public HttpRequestHandler(HttpClient httpClient, TRequestBuilder requestBuilder)
        {
            _httpClient = httpClient;
            _requestBuilder = requestBuilder;
        }

        /// <summary>
        /// Sends the specified HTTP request builder.
        /// </summary>
        /// <param name="httpRequestBuilder">The HTTP request builder.</param>
        /// <param name="httpCompletionOption">The HTTP completion option.</param>
        /// <returns>System.Threading.Tasks.Task&lt;SeaweedFs.Infrastructure.Http.HttpRequestResult&gt;.</returns>
        /// <exception cref="HttpRequestResult">response.IsSuccessStatusCode, response.StatusCode, response.Content</exception>
        public async Task<HttpRequestResult> Send(Func<TRequestBuilder, TRequestBuilder> httpRequestBuilder, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
        {
            try
            {
                var httpRequest = httpRequestBuilder(_requestBuilder)
                    .Build();

                var response = await _httpClient.SendAsync(httpRequest, httpCompletionOption);
#if DEBUG
                Console.WriteLine($"\n\n\n{await response.Content.ReadAsStringAsync()}\n\n\n");
#endif
                return new HttpRequestResult(response.IsSuccessStatusCode, response.StatusCode, response.Content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <param name="httpRequestBuilder">The HTTP request builder.</param>
        /// <param name="httpCompletionOption">The HTTP completion option.</param>
        /// <returns>System.Threading.Tasks.Task&lt;System.IO.Content&gt;.</returns>
        public async Task<Stream> GetStream(Func<TRequestBuilder, TRequestBuilder> httpRequestBuilder, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
        {
            try
            {
                var httpRequest = httpRequestBuilder(_requestBuilder)
                    .Build();

                return await _httpClient.GetStreamAsync(httpRequest.RequestUri);
            }
            catch
            {
                throw;
            }
        }
    }
}