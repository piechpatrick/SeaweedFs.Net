// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-10-2021
// ***********************************************************************
namespace SeaweedFs.Infrastructure.Builders
{
    /// <summary>
    /// Interface IRequestBuilder
    /// </summary>
    /// <typeparam name="TRequest">The type of the t request.</typeparam>
    internal interface IRequestBuilder<out TRequest>
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>TRequest.</returns>
        TRequest Build();
    }
}