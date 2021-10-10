// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-10-2021
// ***********************************************************************

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Infrastructure.Http
{
    /// <summary>
    /// Interface IHttpRequestHandler
    /// </summary>
    /// <typeparam name="TRequestBuilder">The type of the t request builder.</typeparam>
    internal interface IHttpRequestHandler<TRequestBuilder> where TRequestBuilder : IHttpRequestBuilder
    {
        /// <summary>
        /// Sends the specified HTTP request builder.
        /// </summary>
        /// <param name="httpRequestBuilder">The HTTP request builder.</param>
        /// <param name="httpCompletionOption">The HTTP completion option.</param>
        /// <returns>Task&lt;HttpRequestResult&gt;.</returns>
        Task<HttpRequestResult> Send(Func<TRequestBuilder, TRequestBuilder> httpRequestBuilder, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <param name="httpRequestBuilder">The HTTP request builder.</param>
        /// <param name="httpCompletionOption">The HTTP completion option.</param>
        /// <returns>Task&lt;Stream&gt;.</returns>
        Task<Stream> GetStream(Func<TRequestBuilder, TRequestBuilder> httpRequestBuilder, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
    }
}