// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-09-2021
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using MimeKit;

namespace SeaweedFs.Filer.Infrastructure.Http
{
    /// <summary>
    /// Class HttpRequestBuilder.
    /// Implements the <see cref="IHttpRequestBuilder" />
    /// </summary>
    /// <seealso cref="IHttpRequestBuilder" />
    internal class HttpRequestBuilder : IHttpRequestBuilder
    {
        /// <summary>
        /// The serializer options
        /// </summary>
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
        /// <summary>
        /// The HTTP request message
        /// </summary>
        private readonly HttpRequestMessage _httpRequestMessage;
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestBuilder"/> class.
        /// </summary>
        public HttpRequestBuilder()
        {
            _httpRequestMessage = new HttpRequestMessage();
        }
        /// <summary>
        /// Withes the method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>IHttpRequestBuilder.</returns>
        public virtual IHttpRequestBuilder WithMethod(HttpMethod method)
        {
            _httpRequestMessage.Method = method;
            return this;
        }
        /// <summary>
        /// Withes the relative URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>IHttpRequestBuilder.</returns>
        public IHttpRequestBuilder WithRelativeUrl(string url)
        {
            _httpRequestMessage.RequestUri = new Uri(url.Replace("//", "/"), UriKind.Relative);
            return this;
        }
        /// <summary>
        /// Withes the header.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>IHttpRequestBuilder.</returns>
        public IHttpRequestBuilder WithHeader(string name, string value)
        {
            _httpRequestMessage.Headers.TryAddWithoutValidation(name, value);
            return this;
        }
        /// <summary>
        /// Withes the headers.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <returns>IHttpRequestBuilder.</returns>
        public IHttpRequestBuilder WithHeaders(IDictionary<string, string> headers)
        {
            foreach (var (name, value) in headers)
            {
                _httpRequestMessage.Headers.Add(name, value);
            }

            return this;
        }
        /// <summary>
        /// Withes the content of the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>IHttpRequestBuilder.</returns>
        public IHttpRequestBuilder WithStreamContent(Stream stream)
        {
            _httpRequestMessage.Content = new StreamContent(stream);
            return this;
        }
        /// <summary>
        /// Withes the content of the multipart form data.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>IHttpRequestBuilder.</returns>
        public IHttpRequestBuilder WithMultipartStreamFormDataContent(Stream stream, string fileName)
        {
            var form = new MultipartFormDataContent($"---{nameof(HttpRequestBuilder)}" + DateTime.Now.ToString(CultureInfo.InvariantCulture));
            var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(MimeTypes.GetMimeType(fileName));
            form.Add(streamContent, "form-data", fileName);
            _httpRequestMessage.Content = form;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>TRequest.</returns>
        public HttpRequestMessage Build() => _httpRequestMessage;
    }
}