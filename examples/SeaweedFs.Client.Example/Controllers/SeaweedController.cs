using Microsoft.AspNetCore.Mvc;
using SeaweedFs.Filer.Store;
using SeaweedFs.Store;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SeaweedFs.Client.Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeaweedController : ControllerBase
    {
        private readonly Stream exampleFileStream = System.IO.File.OpenRead("D://example//invoice.pdf");

        private readonly IFilerStore _filerStore;

        public SeaweedController(IFilerStore filerStore)
        {
            _filerStore = filerStore;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var catalog = _filerStore.GetCatalog("documents");
            var blobs = await catalog.ListAsync();
            foreach (var b in blobs)
            {
                await catalog.DeleteAsync(b);
            }

            var fileName = $"invoice.pdf";
            var blob = new Blob(fileName, exampleFileStream);
            blob.BlobInfo.Headers.Add("Seaweed-dupa","dupa");
            var res = await catalog.PushAsync(blob);
            var content = await res.Content.ReadAsStringAsync();
            using var uploadedBlob = await catalog.GetAsync(fileName);

            //var blobs = await catalog.ListAsync();var blobs = await catalog.ListAsync();
            //foreach (var bi in blobs)
            //{
            //    var b = await catalog.GetAsync(bi);
            //    await using var fs = System.IO.File.Create($"D:\\{bi.Name}");
            //    await b.Content.CopyToAsync(fs);
            //}


            return Ok();
        }
    }
}
