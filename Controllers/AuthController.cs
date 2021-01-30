using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public ActionResult<string> Login([FromBody] UserLoginRequest request)
        {
            var token = _authService.Login(request);
            if (token != null) return token;
            return BadRequest("User validation failed");
        }

        [ValidateToken]
        public ActionResult<string> RefreshToken()
        {
            var token = HttpContext.Request.Headers["X-User-Token"].ToString();
            return _authService.RefreshToken(token);
        }
    }
}
