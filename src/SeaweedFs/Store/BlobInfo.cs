// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using System;
using System.IO;

namespace SeaweedFs.Store
{
    /// <summary>
    /// Class BlobInfo.
    /// </summary>
    public class BlobInfo
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }
        /// <summary>
        /// Gets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; }
        /// <summary>
        /// Gets the modified.
        /// </summary>
        /// <value>The modified.</value>
        public DateTime Modified { get; }
        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>The headers.</value>
        public BlobHeaders Headers { get; } = new BlobHeaders();
        /// <summary>
        /// Initializes a new instance of the <see cref="BlobInfo" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal BlobInfo(string name)
        {
            Name = Path.GetFileName(name);
            Created = DateTime.Now;
            Modified = DateTime.Now; 
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BlobInfo" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="created">The created.</param>
        /// <param name="modified">The modified.</param>
        internal BlobInfo(string name, DateTime created, DateTime modified)
        {
            Created = created;
            Modified = modified;
            Name = Path.GetFileName(name);
        }
    }
}