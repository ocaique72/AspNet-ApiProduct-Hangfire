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
        public IActionResult Get()
        {
            return Ok();
        }

    }
}
