// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-10-2021
// ***********************************************************************
namespace SeaweedFs.Logging.Options
{
    /// <summary>
    /// Class FileOptions.
    /// </summary>
    public class FileOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FileOptions" /> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled { get; set; }
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }
        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>The interval.</value>
        public string Interval { get; set; }
    }
}