using LoginFSD.Server.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginFSD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if(request.Username == "admin" && request.Password == "password")
            {
                // In real appln, you would generate a JWT token or similar
                return Ok(new { Token = "ThisIsASampleToken" });
            }
            return Unauthorized();
        }
    }
}
