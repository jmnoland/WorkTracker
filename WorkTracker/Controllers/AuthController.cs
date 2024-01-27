using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WorkTracker.Models.Requests;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var token = await _authService.Login(request);
            if (token != null)
            {
                return Ok(token);
            }
            return BadRequest("User validation failed");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (request.Validate().Any()) return BadRequest("Request data is invalid");
            await _authService.Register(request);
            return Ok("User registered successfully");
        }

        [AllowAnonymous]
        [HttpPost("demologin")]
        public async Task<IActionResult> DemoLogin()
        {
            var token = await _authService.DemoLogin();
            if (token != null)
            {
                return Ok(token);
            }
            return BadRequest("Demo login failed");
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var token = Request.Headers["Authorization"]
                .FirstOrDefault()?
                .Split(" ")
                .LastOrDefault();
            var newToken = await _authService.RefreshToken(token);
            return Ok(newToken);
        }
    }
}
