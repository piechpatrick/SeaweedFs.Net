// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-09-2021
// ***********************************************************************
namespace SeaweedFs.Filer
{
    /// <summary>
    /// Class SeaweedOptions.
    /// </summary>
    public class SeaweedOptions
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the master URL.
        /// </summary>
        /// <value>The master URL.</value>
        public string MasterUrl { get; set; }
        /// <summary>
        /// Gets or sets the name of the master HTTP client.
        /// </summary>
        /// <value>The name of the master HTTP client.</value>
        public string MasterHttpClientName { get; set; }
        /// <summary>
        /// Gets or sets the filerService URL.
        /// </summary>
        /// <value>The filerService URL.</value>
        public string FilerUrl { get; set; }
        /// <summary>
        /// Gets or sets the name of the filerService HTTP client.
        /// </summary>
        /// <value>The name of the filerService HTTP client.</value>
        public string FilerHttpClientName { get; set; }
    }
}