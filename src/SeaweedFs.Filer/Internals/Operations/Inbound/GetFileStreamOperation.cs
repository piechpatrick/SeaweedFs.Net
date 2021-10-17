// ***********************************************************************
// Assembly         : SeaweedFs.Filer
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using SeaweedFs.Filer.Internals.Operations.Abstractions;
using SeaweedFs.Operations;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Filer.Internals.Operations.Inbound
{
    /// <summary>
    /// Class GetFileStreamOperation.
    /// Implements the <see cref="OperationBase" />
    /// Implements the <see cref="Stream" />
    /// </summary>
    /// <seealso cref="OperationBase" />
    /// <seealso cref="Stream" />
    internal class GetFileStreamOperation : OperationBase, IFilerOperation<(HttpResponseMessage, Stream)>
    {
        /// <summary>
        /// The path
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// The stream
        /// </summary>
        private Stream _stream = new MemoryStream();

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFileStreamOperation" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="progress">The progress.</param>
        internal GetFileStreamOperation(string path, IProgress<int> progress = null)
        : base(progress)
        {
            _path = path;
        }

        /// <summary>
        /// Executes the specified filerClient.
        /// </summary>
        /// <param name="filerClient">The filerClient.</param>
        /// <returns>Task&lt;TResult&gt;.</returns>
        async Task<(HttpResponseMessage, Stream)> IFilerOperation<(HttpResponseMessage, Stream)>.Execute(IFilerClient filerClient)
        {
            var response = await filerClient.SendAsync(HttpRequestBuilder
                .WithMethod(HttpMethod.Get)
                .WithRelativeUrl(_path)
                .Build(), HttpCompletionOption.ResponseHeadersRead);
            _stream = await response.Content.ReadAsStreamAsync();
            if (_progress != null)
                StartReportingProgress();
            return (response, _stream);
        }
        /// <summary>
        /// Reports the progress.
        /// </summary>
        /// <returns>Task.</returns>
        protected override async Task ReportProgress()
        {
            var prevPos = -1;
            _progress?.Report(0);
            while (_stream.Position < _stream.Length)
            {
                int pos = (int)Math.Round(100 * (_stream.Position / (double)_stream.Length));
                if (pos != prevPos)
                    _progress?.Report(pos);
                prevPos = pos;
                await Task.Delay(10);
            }
            _progress?.Report(100);
        }
    }
}