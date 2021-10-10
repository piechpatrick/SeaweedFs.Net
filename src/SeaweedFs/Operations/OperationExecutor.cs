// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-10-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-10-2021
// ***********************************************************************
using SeaweedFs.Infrastructure.Abstractions;

namespace SeaweedFs.Operations
{
    /// <summary>
    /// Class OperationExecutor.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OperationExecutor<T> where T : IOperator
    {
        /// <summary>
        /// The operator
        /// </summary>
        protected readonly T _operator;
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationExecutor{T}"/> class.
        /// </summary>
        /// <param name="operator">The operator.</param>
        public OperationExecutor(T @operator)
        {
            _operator = @operator;
        }
    }
}