using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Client.Infrastructure.Http
{
    internal class HttpRequestHandler<TRequestBuilder> : IHttpRequestHandler<TRequestBuilder> where TRequestBuilder : IHttpRequestBuilder
    {

        protected readonly HttpClient _httpClient;
        private readonly TRequestBuilder _requestBuilder;

        public HttpRequestHandler(HttpClient httpClient, TRequestBuilder requestBuilder)
        {
            _httpClient = httpClient;
            _requestBuilder = requestBuilder;
        }

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