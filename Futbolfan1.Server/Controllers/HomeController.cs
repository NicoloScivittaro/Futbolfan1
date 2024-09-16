using Microsoft.AspNetCore.Mvc;

namespace FutbolFan1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok(new { message = "Benvenuto in FutbolFan!" });
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            return Ok(new { message = "Informazioni su FutbolFan." });
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return Ok(new { message = "Contatti FutbolFan." });
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            return StatusCode(500, new { message = "Si è verificato un errore." });
        }
    }
}
