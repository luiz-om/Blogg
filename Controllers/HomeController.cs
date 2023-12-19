using Microsoft.AspNetCore.Mvc;

namespace Blogg.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok(new {
                status= "API Rodando"
            });
        }

    }
}