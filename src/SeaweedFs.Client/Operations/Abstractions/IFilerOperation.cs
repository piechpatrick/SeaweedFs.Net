// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-09-2021
// ***********************************************************************

using System.Threading.Tasks;
using SeaweedFs.Filer.Infrastructure.Abstractions;

namespace SeaweedFs.Filer.Operations.Abstractions
{
    /// <summary>
    /// Interface IFilerOperation
    /// Implements the <see cref="SeaweedFs.Client.Operations.Abstractions.IOperation{SeaweedFs.Client.Infrastructure.Abstractions.IFiler}" />
    /// </summary>
    /// <typeparam name="TResult">The type of the t result.</typeparam>
    /// <seealso cref="SeaweedFs.Client.Operations.Abstractions.IOperation{SeaweedFs.Client.Infrastructure.Abstractions.IFiler}" />
    internal interface IFilerOperation<TResult> : IOperation<IFilerService>
    {
        /// <summary>
        /// Executes the specified filerService.
        /// </summary>
        /// <param name="filerService">The filerService.</param>
        /// <returns>Task&lt;TResult&gt;.</returns>
        Task<TResult> Execute(IFilerService filerService);
    }
}