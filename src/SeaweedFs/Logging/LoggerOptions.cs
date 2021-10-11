// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using System.Collections.Generic;
using SeaweedFs.Logging.Options;

namespace SeaweedFs.Logging
{
    /// <summary>
    /// Class LoggerOptions.
    /// </summary>
    public class LoggerOptions
    {
        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static LoggerOptions Default => new LoggerOptions
        {
            Level = "information",
            ExcludePaths = new List<string>(){ "/metrics" },
            Console = new ConsoleOptions{Enabled = true},
            File = new FileOptions
            {
                Enabled = true,
                Path = "logs/logs.txt",
                Interval = "day"
            }
        };
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
        public IDictionary<string, string> MinimumLevelOverrides { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the exclude paths.
        /// </summary>
        /// <value>The exclude paths.</value>
        public IEnumerable<string> ExcludePaths { get; set; } = new List<string>();
        /// <summary>
        /// Gets or sets the exclude properties.
        /// </summary>
        /// <value>The exclude properties.</value>
        public IEnumerable<string> ExcludeProperties { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public IDictionary<string, object> Tags { get; set; } = new Dictionary<string, object>();
    }
}