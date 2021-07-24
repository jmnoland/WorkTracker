using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<string>> Login([FromBody] UserLoginRequest request)
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
        public async Task<ActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _authService.Register(request);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("demologin")]
        public async Task<ActionResult<string>> DemoLogin()
        {
            var token = await _authService.DemoLogin();
            if (token != null)
            {
                return Ok(token);
            }
            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<string>> RefreshToken()
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
