using Microsoft.AspNetCore.Mvc;

namespace AddSolBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 1. Check the hardcoded credentials (The "Gatekeeper")
            if (request.Username == "admin" && request.Password == "password123")
            {
                // 2. Return a "Fake" Token (In real life, this would be a real JWT)
                return Ok(new { token = "fake-jwt-token-12345", message = "Login Successful" });
            }

            return Unauthorized(new { message = "Invalid Username or Password" });
        }
    }

    // A small helper class to hold the data sent from React
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
