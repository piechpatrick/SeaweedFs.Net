using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using SeaweedFs.Client.Infrastructure.Builders;

namespace SeaweedFs.Client.Infrastructure.Http
{
    internal interface IHttpRequestBuilder : IRequestBuilder<HttpRequestMessage>
    {
        IHttpRequestBuilder WithMethod(HttpMethod method);
        IHttpRequestBuilder WithRelativeUrl(string url);
        IHttpRequestBuilder WithHeader(string name, string value);
        IHttpRequestBuilder WithHeaders(IDictionary<string, string> headers);
        IHttpRequestBuilder WithStreamContent(Stream stream);
        IHttpRequestBuilder WithMultipartFormDataContent(Stream stream, string fileName);
    }
}