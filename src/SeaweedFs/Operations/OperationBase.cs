// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using SeaweedFs.Http;
using System;
using System.Threading.Tasks;

namespace SeaweedFs.Operations
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
        /// The progress
        /// </summary>
        protected readonly IProgress<int> _progress;
        /// <summary>
        /// Gets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationBase" /> class.
        /// </summary>
        /// <param name="progress">The progress.</param>
        protected OperationBase(IProgress<int> progress = null)
        {
            _progress = progress;
            HttpRequestBuilder = new HttpRequestBuilder();
            Created = DateTime.Now;
        }
        /// <summary>
        /// Starts the report progress.
        /// </summary>
        protected virtual async void StartReportingProgress()
        {
            await Task.Factory.StartNew(ReportProgress).ConfigureAwait(false);
        }
        /// <summary>
        /// Reports the progress.
        /// </summary>
        /// <returns>Task.</returns>
        protected virtual Task ReportProgress()
        {
            _progress?.Report(100);
            return Task.CompletedTask;
        }
    }
}