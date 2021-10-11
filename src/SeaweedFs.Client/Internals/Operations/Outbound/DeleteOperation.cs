// ***********************************************************************
// Assembly         : SeaweedFs.Filer
// Author           : piechpatrick
// Created          : 10-11-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************
using SeaweedFs.Filer.Internals.Operations.Abstractions;
using SeaweedFs.Operations;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Filer.Internals.Operations.Outbound
{
    /// <summary>
    /// Class DeleteOperation.
    /// Implements the <see cref="SeaweedFs.Operations.OperationBase" />
    /// Implements the <see cref="SeaweedFs.Filer.Internals.Operations.Abstractions.IFilerOperation{System.Boolean}" />
    /// </summary>
    /// <seealso cref="SeaweedFs.Operations.OperationBase" />
    /// <seealso cref="SeaweedFs.Filer.Internals.Operations.Abstractions.IFilerOperation{System.Boolean}" />
    internal class DeleteOperation : OperationBase, IFilerOperation<bool>
    {
        /// <summary>
        /// The path
        /// </summary>
        private readonly string _path;


        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteOperation" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        internal DeleteOperation(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Executes the specified filerClient.
        /// </summary>
        /// <param name="filerClient">The filerClient.</param>
        /// <returns>Task&lt;TResult&gt;.</returns>
        async Task<bool> IFilerOperation<bool>.Execute(IFilerClient filerClient)
        {
            var response = await filerClient.SendAsync(this.BuildRequest());
            return response.IsSuccessStatusCode;
        }
        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>HttpRequestMessage.</returns>
        protected virtual HttpRequestMessage BuildRequest()
        {
            return HttpRequestBuilder.WithRelativeUrl(_path)
                .WithMethod(HttpMethod.Delete)
                .Build();
        }
    }
}