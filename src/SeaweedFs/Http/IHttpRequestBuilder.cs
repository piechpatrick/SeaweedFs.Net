// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-10-2021
// ***********************************************************************

using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using SeaweedFs.Builders;

namespace SeaweedFs.Http
{
    /// <summary>
    /// Interface IHttpRequestBuilder
    /// Implements the <see cref="SeaweedFs.Client.Infrastructure.Builders.IRequestBuilder{System.Net.Http.HttpRequestMessage}" />
    /// </summary>
    /// <seealso cref="SeaweedFs.Client.Infrastructure.Builders.IRequestBuilder{System.Net.Http.HttpRequestMessage}" />
    internal interface IHttpRequestBuilder : IRequestBuilder<HttpRequestMessage>
    {
        /// <summary>
        /// Withes the method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>IHttpRequestBuilder.</returns>
        IHttpRequestBuilder WithMethod(HttpMethod method);
        /// <summary>
        /// Withes the relative URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>IHttpRequestBuilder.</returns>
        IHttpRequestBuilder WithRelativeUrl(string url);
        /// <summary>
        /// Withes the header.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>IHttpRequestBuilder.</returns>
        IHttpRequestBuilder WithHeader(string name, string value);
        /// <summary>
        /// Withes the headers.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <returns>IHttpRequestBuilder.</returns>
        IHttpRequestBuilder WithHeaders(IDictionary<string, string> headers);
        /// <summary>
        /// Withes the content of the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>IHttpRequestBuilder.</returns>
        IHttpRequestBuilder WithStreamContent(Stream stream);
        /// <summary>
        /// Withes the content of the multipart form data.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>IHttpRequestBuilder.</returns>
        IHttpRequestBuilder WithMultipartStreamFormDataContent(Stream stream, string fileName);
    }
}