// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-09-2021
// ***********************************************************************

using SeaweedFs.Filer.Infrastructure.Abstractions;

namespace SeaweedFs.Filer.Operations.Abstractions
{
    /// <summary>
    /// Interface IOperation
    /// </summary>
    /// <typeparam name="TOperator">The type of the t operator.</typeparam>
    internal interface IOperation<in TOperator> where TOperator : IOperator
    {

    }
}