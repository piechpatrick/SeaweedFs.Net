using SeaweedFs.Client.Infrastructure;
using SeaweedFs.Client.Infrastructure.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Client.Core
{
    internal class MasterHttpRequestHandler : HttpRequestHandler<IHttpRequestBuilder>, IMasterHttpRequestHandler
    {
        public MasterHttpRequestHandler(IHttpClientFactory httpClientFactory, SeaweedOptions options)
            : base(httpClientFactory.CreateClient(options.MasterHttpClientName), new HttpRequestBuilder())
        {

        }

        public async Task AssignFile()
        {
            var result = await this.Send(builder => builder
                .WithMethod(HttpMethod.Get)
                .WithRelativeUrl("dir/assign"));
        }
    }
}