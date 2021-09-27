using SeaweedFs.Client.Infrastructure;
using SeaweedFs.Client.Infrastructure.Http;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Client.Core
{
    internal class FilerHttpRequestHandler : HttpRequestHandler<IHttpRequestBuilder>, IFilerHttpRequestHandler
    {
        public FilerHttpRequestHandler(IHttpClientFactory httpClientFactory, SeaweedOptions options)
            : base(httpClientFactory.CreateClient(options.FilerHttpClientName), new HttpRequestBuilder())
        {

        }

        public async Task<bool> IsFileExist(string filePath)
        {
            return (await this.Send(builder => builder
                .WithMethod(HttpMethod.Get)
                .WithRelativeUrl(filePath), HttpCompletionOption.ResponseHeadersRead))?.StatusCode == HttpStatusCode.OK;
        }
        public async Task UploadFile(string path, Stream stream)
        {
            var result = await this.Send(builder => builder
                .WithRelativeUrl(path)
                .WithMethod(HttpMethod.Post)
                .WithMultipartFormDataContent(stream, Path.GetFileName(path)));
        }

        public async Task RemoveFile(string path)
        {
            var result = await this.Send(builder => builder
                .WithRelativeUrl(path)
                .WithMethod(HttpMethod.Delete));
        }
    }
}