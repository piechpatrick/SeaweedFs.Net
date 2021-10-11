// ***********************************************************************
// Assembly         : SeaweedFs.Client.Example
// Author           : piechpatrick
// Created          : 10-11-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;
using SeaweedFs.Filer.Store;
using SeaweedFs.Store;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaweedFs.Client.Example.Controllers
{
    /// <summary>
    /// Class SeaweedController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class SeaweedController : ControllerBase
    {
        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>System.Byte[].</returns>
        private async Task<FileStream> GetStream(uint size)
        {
            var memoryStream = new MemoryStream();
            StreamWriter sr = new StreamWriter(memoryStream);
            for (int i = 0; i < size; i++)
                    await sr.WriteAsync("COFFEE~~BABE~~");
            await memoryStream.FlushAsync();
            memoryStream.Position = 0;
            var fileStream = new FileStream(Path.GetRandomFileName(), FileMode.Create, FileAccess.ReadWrite);
            await memoryStream.CopyToAsync(fileStream);
            await memoryStream.DisposeAsync();
            fileStream.Position = 0;
            return fileStream;
        }

        /// <summary>
        /// The filer store
        /// </summary>
        private readonly IFilerStore _filerStore;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<SeaweedController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeaweedController" /> class.
        /// </summary>
        /// <param name="filerStore">The filer store.</param>
        /// <param name="logger">The logger.</param>
        public SeaweedController(IFilerStore filerStore, ILogger<SeaweedController> logger)
        {
            _filerStore = filerStore;
            _logger = logger;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //create fakeFileStream
            using (var outboundFileStream = await GetStream(0xC0FFEE))
            {

                _logger.LogInformation($"File Created: {outboundFileStream.Name}");

                //get catalog
                var catalog = _filerStore.GetCatalog("documents");

                //delete all files
                Parallel.ForEach(await catalog.ListAsync(), (bi) => catalog.DeleteAsync(bi).ConfigureAwait(false));

                //create simple blob
                var blob = new Blob($"{Guid.NewGuid()}.txt", outboundFileStream);

                //add custom header value
                blob.BlobInfo.Headers.Add("Seaweed-OwnerId", new[] { $"{Guid.NewGuid()}" });

                //push blob
                await catalog.PushAsync(blob, new Progress<int>((p) =>
                {
                    _logger.LogInformation($"Upload progress: {p} %");
                }));

                using (var uploadedBlob = await catalog.GetAsync(blob.BlobInfo.Name,
                    new Progress<int>((p) => { _logger.LogInformation($"Download progress: {p} %"); })))
                {
                    //read
                    using (var sr = new StreamReader(uploadedBlob.Content))
                    {
                        string line;
                        while ((line = await sr.ReadLineAsync()) != null)
                        {

                        }
                    }


                }

                System.IO.File.Delete(outboundFileStream.Name);
                _logger.LogInformation($"File Deleted: {outboundFileStream.Name}");
            }
            
            return Ok();
        }
    }
}
