// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using System;
using System.IO;
using System.Threading.Tasks;

namespace SeaweedFs.Operations
{
    /// <summary>
    /// Class OutboundStreamOperation.
    /// Implements the <see cref="OperationBase" />
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="OperationBase" />
    /// <seealso cref="System.IDisposable" />
    internal abstract class OutboundStreamOperation : OperationBase, IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// The stream
        /// </summary>
        protected readonly Stream _stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutboundStreamOperation" /> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="_progress">The progress.</param>
        protected OutboundStreamOperation(Stream stream, IProgress<int> _progress = null)
            : base(_progress)
        {
            _stream = stream;
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _stream?.Dispose();
        }
        /// <summary>
        /// Disposes the asynchronous.
        /// </summary>
        /// <returns>ValueTask.</returns>
        public ValueTask DisposeAsync()
        {
            return _stream?.DisposeAsync() ?? ValueTask.CompletedTask;
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
                await Task.Delay(100);
            }
        }
    }
}