// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-10-2021
// ***********************************************************************
using SeaweedFs.Filer.Internals.Operations.Abstractions;
using SeaweedFs.Operations;
using System.Threading.Tasks;

namespace SeaweedFs.Filer.Internals.Operations
{
    /// <summary>
    /// Class FilerOperationsExecutor.
    /// Implements the <see cref="SeaweedFs.Operations.OperationExecutor{SeaweedFs.Filer.Internals.FilerClient}" />
    /// </summary>
    /// <seealso cref="SeaweedFs.Operations.OperationExecutor{SeaweedFs.Filer.Internals.FilerClient}" />
    internal class FilerOperationsExecutor : OperationExecutor<FilerClient>, IFilerOperationsExecutor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilerOperationsExecutor" /> class.
        /// </summary>
        /// <param name="operator">The operator.</param>
        public FilerOperationsExecutor(FilerClient @operator) : base(@operator)
        {
        }

        /// <summary>
        /// Executes the specified operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation">The operation.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public Task<T> Execute<T>(IFilerOperation<T> operation)
        {
            return operation.Execute(_operator);
        }
    }
}