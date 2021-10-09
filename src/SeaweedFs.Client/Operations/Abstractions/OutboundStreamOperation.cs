// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-09-2021
// ***********************************************************************

using System;
using System.IO;
using System.Threading.Tasks;

namespace SeaweedFs.Filer.Operations.Abstractions
{
    /// <summary>
    /// Class OutboundStreamOperation.
    /// Implements the <see cref="OperationBase" />
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="OperationBase" />
    /// <seealso cref="System.IDisposable" />
    internal abstract class OutboundStreamOperation : OperationBase, IDisposable
    {
        /// <summary>
        /// The stream
        /// </summary>
        protected readonly Stream _stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutboundStreamOperation"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        protected OutboundStreamOperation(Stream stream)
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
    }
}