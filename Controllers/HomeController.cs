using Blog.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace apiDesafio.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        [ApiKey]
        public IActionResult Get([FromQuery] string api_key)
        {
            return Ok();
        }
    }
}
