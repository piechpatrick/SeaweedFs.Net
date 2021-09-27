using SeaweedFs.Client.Infrastructure.Http;
using System.IO;
using System.Threading.Tasks;

namespace SeaweedFs.Client.Core
{
    internal interface IFilerHttpRequestHandler : IHttpRequestHandler<IHttpRequestBuilder>
    {
        Task<bool> IsFileExist(string filePath);
        Task UploadFile(string path, Stream stream);
        Task RemoveFile(string path);
    }
}