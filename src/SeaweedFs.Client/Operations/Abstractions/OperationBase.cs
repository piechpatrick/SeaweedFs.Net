// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-09-2021
// ***********************************************************************

using System;
using SeaweedFs.Filer.Infrastructure.Http;

namespace SeaweedFs.Filer.Operations.Abstractions
{
    /// <summary>
    /// Class OperationBase.
    /// </summary>
    internal abstract class OperationBase
    {
        /// <summary>
        /// The HTTP request builder
        /// </summary>
        protected readonly IHttpRequestBuilder HttpRequestBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationBase"/> class.
        /// </summary>
        protected OperationBase()
        {
            HttpRequestBuilder = new HttpRequestBuilder();
            Created = DateTime.Now;
        }
        /// <summary>
        /// Gets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; }
    }
}