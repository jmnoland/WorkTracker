using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Models.DTOs;
using WorkTracker.Models.Requests;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetUsers([FromRoute] int teamId)
        {
            var response = await _userService.GetUsersByTeamId(teamId);
            if (response == null) return NoContent();
            return Ok(response);
        }

        [HttpGet("details/{userId}")]
        public async Task<IActionResult> GetUserDetails([FromRoute] int userId)
        {
            var response = await _userService.GetUserDetail(userId);
            if (response == null) return NoContent();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (request.Validate().Count > 0) return BadRequest("Invalid request");
            try
            {
                await _userService.CreateUser(request);
                return Ok("User created successfully");
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Role does not exist");
            }
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserRequest request)
        {
            if (request.Validate().Count > 0) return BadRequest("Invalid request");

            await _userService.RegisterUser(request);
            return Ok("User registered successfully");
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            if (request.Validate().Count > 0) return BadRequest("Invalid request");
            var result = await _userService.UpdateUser(request);
            if (result == null) return BadRequest("User update failed");
            return Ok("User updated successfully");
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> RemoveUser([FromRoute] int userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return Ok("User removed successfully");
            }
            catch
            {
                return BadRequest("Unable to remove user");
            }
        }
    }
}
