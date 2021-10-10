// ***********************************************************************
// Assembly         : SeaweedFs.Filer
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-10-2021
// ***********************************************************************
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Filer.Store.Catalog
{
    /// <summary>
    /// Interface IFilerCatalog
    /// </summary>
    public interface IFilerCatalog
    {
        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <value>The directory.</value>
        string Directory { get; }
        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="stream">The stream.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> Upload(string fileName, Stream stream);
    }
}