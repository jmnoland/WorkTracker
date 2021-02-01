using Microsoft.AspNetCore.Http;
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
            if (token != null)
            {
                AddRefreshToken(token);
                return token;
            }
            return BadRequest("User validation failed");
        }

        [ValidateToken]
        public ActionResult<string> RefreshToken()
        {
            var token = Request.Cookies["X-User-Token"];
            var newToken = _authService.RefreshToken(token);
            AddRefreshToken(newToken);
            return newToken;
        }


        private void AddRefreshToken(string token)
        {
            var options = new CookieOptions();
            options.SameSite = SameSiteMode.Strict;
            Response.Cookies.Append("X-User-Token", token, options);
        }
    }
}
