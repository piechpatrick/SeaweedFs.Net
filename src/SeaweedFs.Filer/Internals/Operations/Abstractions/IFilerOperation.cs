// ***********************************************************************
// Assembly         : SeaweedFs.Filer
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using System.Threading.Tasks;
using SeaweedFs.Operations;

namespace SeaweedFs.Filer.Internals.Operations.Abstractions
{
    /// <summary>
    /// Interface IFilerOperation
    /// Implements the <see cref="IFilerClient" />
    /// </summary>
    /// <typeparam name="TResult">The type of the t result.</typeparam>
    /// <seealso cref="IFilerClient" />
    internal interface IFilerOperation<TResult> : IOperation<IFilerClient>
    {
        /// <summary>
        /// Executes the specified filerClient.
        /// </summary>
        /// <param name="filerClient">The filerClient.</param>
        /// <returns>Task&lt;TResult&gt;.</returns>
        Task<TResult> Execute(IFilerClient filerClient);
    }
}