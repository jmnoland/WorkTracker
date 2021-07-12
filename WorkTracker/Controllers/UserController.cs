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
            return Ok(await _userService.GetUsersByTeamId(teamId));
        }

        [HttpGet("details/{userId}")]
        public async Task<ActionResult<UserDetail>> GetUserDetails([FromRoute] int userId)
        {
            return Ok(await _userService.GetUserDetail(userId));
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (request.Validate().Count > 0) return BadRequest("Invalid request");
            try
            {
                await _userService.CreateUser(request);
                return Ok("User created successfully");
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Role does not exist.");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] CreateUserRequest request)
        {
            if (request.Validate().Count > 0) return BadRequest("Invalid request");

            await _userService.RegisterUser(request);
            return Ok("User registered successfully");
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            if (request.Validate().Count > 0) return BadRequest("Invalid request");

            try
            {
                await _userService.UpdateUser(request);
                return Ok("User updated successfully");
            }
            catch
            {
                throw new Exception("Update failed");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> RemoveUser([FromRoute] int userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return Ok("User removed successfully");
            }
            catch
            {
                return BadRequest("Unable to remove user.");
            }
        }
    }
}
