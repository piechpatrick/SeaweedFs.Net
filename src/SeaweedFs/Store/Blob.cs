// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.IO;

namespace SeaweedFs.Store
{
    /// <summary>
    /// Class Blob.
    /// </summary>
    public class Blob : IDisposable
    {
        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>The content.</value>
        public Stream Content { get; }
        /// <summary>
        /// Gets the information.
        /// </summary>
        /// <value>The information.</value>
        public BlobInfo BlobInfo { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Blob" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="content">The content.</param>
        /// <exception cref="System.InvalidOperationException">Blob</exception>
        public Blob(string name, Stream content)
        {
            if (string.IsNullOrEmpty(name) || !content.CanRead)
                throw new InvalidOperationException(nameof(Blob));
            Content = content;
            BlobInfo = new BlobInfo(name);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Blob" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="bytes">The bytes.</param>
        /// <exception cref="System.InvalidOperationException">Blob</exception>
        public Blob(string name, byte[] bytes)
        {
            if (string.IsNullOrEmpty(name) || bytes.Length == 0)
                throw new InvalidOperationException(nameof(Blob));
            Content = new MemoryStream(bytes);
            BlobInfo = new BlobInfo(name);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Blob"/> class.
        /// </summary>
        /// <param name="blobInfo">The BLOB information.</param>
        /// <param name="content">The content.</param>
        /// <exception cref="System.InvalidOperationException">Blob</exception>
        public Blob(BlobInfo blobInfo, Stream content)
        {
            if (string.IsNullOrEmpty(blobInfo.Name) || !content.CanRead)
                throw new InvalidOperationException(nameof(Blob));
            Content = content;
            BlobInfo = blobInfo;
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Content?.Dispose();
        }
    }
}