using Blog.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace apiDesafio.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get()
        {
            return Ok("Api online!");
        }
    }
}
