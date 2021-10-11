// ***********************************************************************
// Assembly         : SeaweedFs.Filer
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************
using SeaweedFs.Filer.Store.Catalog;

namespace SeaweedFs.Filer.Store
{
    /// <summary>
    /// Interface IFilerStore
    /// </summary>
    public interface IFilerStore
    {
        /// <summary>
        /// Gets the catalog.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <returns>IFilerCatalog.</returns>
        IFilerCatalog GetCatalog(string directory);
    }
}