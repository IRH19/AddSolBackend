using Microsoft.AspNetCore.Mvc;

namespace AddSolBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            // Simple check (Hardcoded for now)
            if (login.Username == "admin" && login.Password == "password123")
            {
                return Ok(new { 
                    token = "fake-jwt-token-for-demo",
                    message = "Login Successful" 
                });
            }
            return Unauthorized();
        }
    }

    // FIX: Added '?' to make these properties nullable
    // This tells C#: "It's okay if these are momentarily null while parsing JSON."
    public class LoginModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
