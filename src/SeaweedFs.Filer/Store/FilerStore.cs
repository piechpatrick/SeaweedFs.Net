// ***********************************************************************
// Assembly         : SeaweedFs.Filer
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************
using SeaweedFs.Filer.Internals;
using SeaweedFs.Filer.Internals.Operations;
using SeaweedFs.Filer.Store.Catalog;

namespace SeaweedFs.Filer.Store
{
    /// <summary>
    /// Class FilerStore.
    /// Implements the <see cref="SeaweedFs.Filer.Store.IFilerStore" />
    /// </summary>
    /// <seealso cref="SeaweedFs.Filer.Store.IFilerStore" />
    internal class FilerStore : IFilerStore
    {
        /// <summary>
        /// The filer client
        /// </summary>
        private readonly IFilerClient _filerClient;
        /// <summary>
        /// The executor
        /// </summary>
        private readonly IFilerOperationsExecutor _executor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilerStore" /> class.
        /// </summary>
        /// <param name="filerClient">The filer client.</param>
        /// <param name="executor">The executor.</param>
        public FilerStore(IFilerClient filerClient, IFilerOperationsExecutor executor)
        {
            _filerClient = filerClient;
            _executor = executor;
        }

        /// <summary>
        /// Gets the catalog.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <returns>IFilerCatalog.</returns>
        public IFilerCatalog GetCatalog(string directory)
        {
            if (!directory.EndsWith("/")) directory += "/";
            return new FilerCatalog(directory, this, _executor);
        }
    }
}