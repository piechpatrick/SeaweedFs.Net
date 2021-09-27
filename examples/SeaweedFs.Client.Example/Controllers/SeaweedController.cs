using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;

namespace SeaweedFs.Client.Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeaweedController : ControllerBase
    {
        private readonly ILogger<SeaweedController> _logger;
        private readonly ISeaweed _seaweed;

        public SeaweedController(ILogger<SeaweedController> logger, ISeaweed seaweed)
        {
            _logger = logger;
            _seaweed = seaweed;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes("hello world"));
            _seaweed.FilerClient.UploadFile($"directory/{Guid.NewGuid()}.txt", ms);
            return Created(nameof(Get), DateTime.UtcNow);
        }
    }
}
