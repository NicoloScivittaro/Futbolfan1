using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutbolFan1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpGet("protected")]
        [Authorize]
        public IActionResult ProtectedRoute()
        {
            return Ok("Access granted to protected route");
        }

        [HttpGet("public")]
        public IActionResult PublicRoute()
        {
            return Ok("This route is accessible without authentication");
        }
    }
}
