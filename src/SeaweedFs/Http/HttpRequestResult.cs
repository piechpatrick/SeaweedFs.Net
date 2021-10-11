// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using System;
using System.Net;
using System.Net.Http;

namespace SeaweedFs.Http
{
    /// <summary>
    /// Class HttpRequestResult.
    /// </summary>
    internal class HttpRequestResult
    {
        /// <summary>
        /// Gets a value indicating whether this instance is success.
        /// </summary>
        /// <value><c>true</c> if this instance is success; otherwise, <c>false</c>.</value>
        public bool IsSuccess { get; }
        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>The status code.</value>
        public HttpStatusCode StatusCode { get; }
        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>The content.</value>
        public HttpContent Content { get; }
        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestResult" /> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public HttpRequestResult(Exception exception = null)
        {
            IsSuccess = false;
            Exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestResult" /> class.
        /// </summary>
        /// <param name="isSuccess">if set to <c>true</c> [is success].</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public HttpRequestResult(bool isSuccess, HttpStatusCode statusCode, HttpContent content)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Content = content;
        }
    }
}