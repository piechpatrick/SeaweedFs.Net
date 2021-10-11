using Microsoft.AspNetCore.Mvc;
using SeaweedFs.Filer.Store;
using SeaweedFs.Store;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SeaweedFs.Client.Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeaweedController : ControllerBase
    {
        private readonly Stream exampleFileStream = new MemoryStream(Encoding.UTF8.GetBytes("hello world"));

        private readonly IFilerStore _filerStore;

        public SeaweedController(IFilerStore filerStore)
        {
            _filerStore = filerStore;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //get catalog
            var catalog = _filerStore.GetCatalog("documents");

            //delete all files
            var blobInfos = await catalog.ListAsync();
            foreach (var bi in blobInfos)
            {
                await catalog.DeleteAsync(bi);
            }

            //create simple blob
            var blob = new Blob($"{Guid.NewGuid()}.txt", exampleFileStream);

            //add custom header value
            blob.BlobInfo.Headers.Add("Seaweed-OwnerId", new[] {$"{Guid.NewGuid()}"});

            //push blob
            await catalog.PushAsync(blob);

            //get blob
            using var uploadedBlob = await catalog.GetAsync(blob.BlobInfo.Name);

            return Ok();
        }
    }
}
