// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using System;
using System.Threading;
using System.Threading.Tasks;
using SeaweedFs.Http;

namespace SeaweedFs.Operations
{
    /// <summary>
    ///     Class OperationBase.
    /// </summary>
    internal abstract class OperationBase
    {
        protected readonly CancellationToken _cancellationToken;

        /// <summary>
        ///     The progress
        /// </summary>
        protected readonly IProgress<int> _progress;

        /// <summary>
        ///     The HTTP request builder
        /// </summary>
        protected readonly IHttpRequestBuilder HttpRequestBuilder;

        /// <summary>
        ///     Initializes a new instance of the <see cref="OperationBase" /> class.
        /// </summary>
        /// <param name="progress">The progress.</param>
        protected OperationBase(CancellationToken cancellationToken = default, IProgress<int> progress = null)
            : this(cancellationToken)
        {
            _progress = progress;
            HttpRequestBuilder = new HttpRequestBuilder();
            Created = DateTime.Now;
        }

        protected OperationBase(CancellationToken cancellationToken) => _cancellationToken = cancellationToken;

        /// <summary>
        ///     Gets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; }

        /// <summary>
        ///     Starts the report progress.
        /// </summary>
        protected virtual async void StartReportingProgress()
        {
            await Task.Factory.StartNew(ReportProgress).ConfigureAwait(false);
        }

        /// <summary>
        ///     Reports the progress.
        /// </summary>
        /// <returns>Task.</returns>
        protected virtual Task ReportProgress()
        {
            _progress?.Report(100);
            return Task.CompletedTask;
        }
    }
}