using System;
using Microsoft.AspNetCore.Mvc;
using SeaweedFs.Filer.Store;
using System.IO;

namespace SeaweedFs.Client.Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeaweedController : ControllerBase
    {
        private readonly Stream fileStream = System.IO.File.OpenRead("D://example//invoice.pdf");

        private readonly IFilerStore _filerStore;

        public SeaweedController(IFilerStore filerStore)
        {
            _filerStore = filerStore;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var catalog = _filerStore.GetCatalog("documents");
            catalog.Upload($"{Guid.NewGuid()}.pdf", fileStream);
            return Ok();
        }
    }
}
