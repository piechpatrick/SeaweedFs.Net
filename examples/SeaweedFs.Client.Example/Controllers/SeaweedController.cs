using Microsoft.AspNetCore.Mvc;

namespace SeaweedFs.Client.Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeaweedController : ControllerBase
    {

        public SeaweedController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
