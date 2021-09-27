using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Client.Infrastructure.Http
{
    internal interface IHttpRequestHandler<TRequestBuilder> where TRequestBuilder : IHttpRequestBuilder
    {
        Task<HttpRequestResult> Send(Func<TRequestBuilder, TRequestBuilder> httpRequestBuilder, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
        Task<Stream> GetStream(Func<TRequestBuilder, TRequestBuilder> httpRequestBuilder, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
    }
}