using SeaweedFs.Client.Core;
using SeaweedFs.Client.Infrastructure;
using System.Net.Http;
using System.Threading.Tasks;
using SeaweedFs.Client.Core.Master;

namespace SeaweedFs.Client
{
    internal class Master : IMaster
    {
        private readonly IMasterHttpRequestHandler _httpRequestHandler;
        private readonly SeaweedOptions _options;

        public Master(IMasterHttpRequestHandler httpRequestHandler, SeaweedOptions options)
        {
            _httpRequestHandler = httpRequestHandler;
            _options = options;
        }

        public async Task AssignFile()
        {
            var result = await _httpRequestHandler.Send(builder => builder
                .WithMethod(HttpMethod.Get)
                .WithRelativeUrl("dir/assign"));
        }
    }
}