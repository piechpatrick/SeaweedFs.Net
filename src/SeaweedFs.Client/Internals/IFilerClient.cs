using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using SeaweedFs.Infrastructure.Abstractions;

namespace SeaweedFs.Filer.Internals
{
    internal interface IFilerClient : IOperator
    {
        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="httpRequestMessage">The HTTP request message.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage);
        /// <summary>
        /// Gets the stream asynchronous.
        /// </summary>
        /// <param name="httpRequestMessage">The HTTP request message.</param>
        /// <returns>Task&lt;Stream&gt;.</returns>
        Task<Stream> GetStreamAsync(HttpRequestMessage httpRequestMessage);
    }
}