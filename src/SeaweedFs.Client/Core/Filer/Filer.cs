using SeaweedFs.Client.Infrastructure;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeaweedFs.Client.Core
{
    internal class Filer : IFiler
    {
        private readonly Func<IFilerHttpRequestHandler> _httpRequestHandlerFactory;
        private readonly SeaweedOptions _options;

        public Filer(Func<IFilerHttpRequestHandler> httpRequestHandlerFactory, SeaweedOptions options)
        {
            _httpRequestHandlerFactory = httpRequestHandlerFactory;
            _options = options;
        }
        public Task<bool> IsFileExist(string filePath)
        {
            return _httpRequestHandlerFactory().IsFileExist(filePath);
        }
        public async Task GetFile(string path)
        {
            if (!await this.IsFileExist(path))
                throw new InvalidOperationException($"File {path} does not exist.");


            var result = await _httpRequestHandlerFactory().GetStream(builder => builder
                .WithMethod(HttpMethod.Get)
                .WithRelativeUrl(path));

            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            result.CopyTo(fileStream);
            fileStream.Dispose();
        }

        public async Task UploadFile(string path, Stream stream)
        {
            if (await this.IsFileExist(path))
                throw new InvalidOperationException($"File {path} already exist.");

            await _httpRequestHandlerFactory().UploadFile(path, stream);
        }

        public Task RemoveFile(string path)
        {
            return _httpRequestHandlerFactory().RemoveFile(path);
        }
    }
}