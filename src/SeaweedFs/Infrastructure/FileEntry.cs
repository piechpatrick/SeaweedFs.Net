// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-11-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************
using System;
using System.IO;

namespace SeaweedFs.Infrastructure
{
    /// <summary>
    /// Class FileEntry.
    /// </summary>
    internal class FileEntry
    {
        /// <summary>
        /// Gets or sets the crtime.
        /// </summary>
        /// <value>The crtime.</value>
        public DateTime Crtime { get; set; }
        /// <summary>
        /// Gets or sets the mtime.
        /// </summary>
        /// <value>The mtime.</value>
        public DateTime Mtime { get; set; }
        /// <summary>
        /// Gets or sets the full path.
        /// </summary>
        /// <value>The full path.</value>
        public string FullPath { get; set; }
        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public uint Mode { get; set; }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Path.GetFileName(FullPath);
        }
    }
}