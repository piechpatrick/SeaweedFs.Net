// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-11-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************
using System.Collections.Generic;

namespace SeaweedFs.Infrastructure.Protocol
{
    /// <summary>
    /// Class DirectoryFileEntriesResponse.
    /// </summary>
    internal class DirectoryFileEntriesResponse
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }
        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        /// <value>The entries.</value>
        public List<FileEntry> Entries { get; set; }
    }
}