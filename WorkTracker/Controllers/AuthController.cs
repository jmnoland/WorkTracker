using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Controllers.Attributes;
using WorkTracker.Models.DTOs;
using WorkTracker.Models.Requests;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var token = await _authService.Login(request);
            if (token != null)
            {
                AddRefreshToken(token);
                return Ok(token);
            }
            return BadRequest("User validation failed");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest("Request data is invalid");
            await _authService.Register(request);
            return Ok();
        }

        [HttpPost("demologin")]
        public async Task<IActionResult> DemoLogin()
        {
            var token = await _authService.DemoLogin();
            if (token != null)
            {
                AddRefreshToken(token);
                return Ok(token);
            }
            return BadRequest("Demo login failed");
        }

        [ValidateToken]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var token = Request.Cookies["X-User-Token"];
            var newToken = await _authService.RefreshToken(token);
            AddRefreshToken(newToken);
            return Ok(newToken);
        }

        [HttpGet]
        public IActionResult CookiesSupported()
        {
            var token = Request.Cookies["X-User-Token"];
            if (token == null) return Ok(false);
            return Ok(true);
        }

        private void AddRefreshToken(string token)
        {
            if (token == null) return;
            var options = new CookieOptions();
            options.SameSite = SameSiteMode.Strict;
            options.HttpOnly = true;
            Response.Cookies.Append("X-User-Token", token, options);
        }
    }
}
