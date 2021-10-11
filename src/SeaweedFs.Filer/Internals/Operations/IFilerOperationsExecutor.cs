// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-10-2021
// ***********************************************************************
using System.Threading.Tasks;
using SeaweedFs.Filer.Internals.Operations.Abstractions;

namespace SeaweedFs.Filer.Internals.Operations
{
    /// <summary>
    /// Interface IFilerOperationsExecutor
    /// </summary>
    internal interface IFilerOperationsExecutor
    {
        /// <summary>
        /// Executes the specified operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation">The operation.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> Execute<T>(IFilerOperation<T> operation);
    }
}