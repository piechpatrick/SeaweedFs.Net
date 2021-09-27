using System.IO;
using System.Threading.Tasks;

namespace SeaweedFs.Client.Core
{
    public interface IFiler
    {
        Task GetFile(string path);
        Task<bool> IsFileExist(string filePath);
        Task UploadFile(string path, Stream stream);
        Task RemoveFile(string path);
    }
}