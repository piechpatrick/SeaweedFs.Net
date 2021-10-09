// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-09-2021
// ***********************************************************************

using System.Collections.Generic;
using SeaweedFs.Filer.Logging.Options;

namespace SeaweedFs.Filer.Logging
{
    /// <summary>
    /// Class LoggerOptions.
    /// </summary>
    public class LoggerOptions
    {
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public string Level { get; set; }
        /// <summary>
        /// Gets or sets the console.
        /// </summary>
        /// <value>The console.</value>
        public ConsoleOptions Console { get; set; }
        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>The file.</value>
        public FileOptions File { get; set; }
        /// <summary>
        /// Gets or sets the minimum level overrides.
        /// </summary>
        /// <value>The minimum level overrides.</value>
        public IDictionary<string, string> MinimumLevelOverrides { get; set; }
        /// <summary>
        /// Gets or sets the exclude paths.
        /// </summary>
        /// <value>The exclude paths.</value>
        public IEnumerable<string> ExcludePaths { get; set; }
        /// <summary>
        /// Gets or sets the exclude properties.
        /// </summary>
        /// <value>The exclude properties.</value>
        public IEnumerable<string> ExcludeProperties { get; set; }
        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public IDictionary<string, object> Tags { get; set; }
    }
}