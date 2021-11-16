// ***********************************************************************
// Assembly         : SeaweedFs.Client.Example
// Author           : piechpatrick
// Created          : 10-11-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-12-2021
// ***********************************************************************

using System;
using System.Buffers;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeaweedFs.Filer.Store;
using SeaweedFs.Store;

namespace SeaweedFs.Client.Example.Controllers
{
    /// <summary>
    ///     Class SeaweedController.
    ///     Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class SeaweedController : ControllerBase
    {
        /// <summary>
        ///     The filer store
        /// </summary>
        private readonly IFilerStore _filerStore;

        /// <summary>
        ///     The logger
        /// </summary>
        private readonly ILogger<SeaweedController> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SeaweedController" /> class.
        /// </summary>
        /// <param name="filerStore">The filer store.</param>
        /// <param name="logger">The logger.</param>
        public SeaweedController(IFilerStore filerStore, ILogger<SeaweedController> logger)
        {
            _filerStore = filerStore;
            _logger = logger;
        }

        /// <summary>
        ///     Generates this instance.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>System.Byte[].</returns>
        private async Task<string> GetFileName(int size)
        {
            var fileName = Path.GetRandomFileName();
            var buffer = ArrayPool<byte>.Shared.Rent(size);
            try
            {
                await System.IO.File.WriteAllBytesAsync(fileName, buffer);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer, true);
            }

            return fileName;
        }

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //create fakeFileStream
            await using (var outboundFileStream = System.IO.File.OpenRead(await GetFileName(0xC0FFEEE)))
            {
                _logger.LogInformation($"File Created: {outboundFileStream.Name}");

                //get catalog
                var catalog = _filerStore.GetCatalog("documents");

                //delete all files
                Parallel.ForEach(await catalog.ListAsync(), bi => catalog.DeleteAsync(bi).ConfigureAwait(false));

                //create simple blob
                var blob = new Blob($"{Guid.NewGuid()}.txt", outboundFileStream);

                //add custom header value
                blob.BlobInfo.Headers.Add("Seaweed-OwnerId", new[] {$"{Guid.NewGuid()}"});

                //push blob
                await catalog.PushAsync(blob,
                    progress: new Progress<int>(p => { _logger.LogInformation($"Upload progress: {p} %"); }));

                using (var uploadedBlob = await catalog.GetAsync(blob.BlobInfo.Name,
                           progress: new Progress<int>(p => { _logger.LogInformation($"Download progress: {p} %"); })))
                {
                    //read
                    using (var sr = new StreamReader(uploadedBlob.Content))
                    {
                        var buffer = ArrayPool<char>.Shared.Rent(128);
                        while (await sr.ReadAsync(buffer) > 0) ArrayPool<char>.Shared.Return(buffer);
                    }
                }

                System.IO.File.Delete(outboundFileStream.Name);
                _logger.LogInformation($"File Deleted: {outboundFileStream.Name}");
            }

            return Ok();
        }
    }
}