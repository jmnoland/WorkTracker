using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Authorize(Roles = "view_user")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var currentUserId = Helper.GetRequestUserId(HttpContext);
            if (currentUserId == null) return BadRequest("UserId missing");
            var userList = await _userService.GetUsersByTeamId((int)currentUserId);
            if (!userList.Any()) return NoContent();
            return Ok(userList);
        }

        [Authorize(Roles = "view_user")]
        [HttpGet("details")]
        public async Task<IActionResult> GetUserDetails()
        {
            var currentUserId = Helper.GetRequestUserId(HttpContext);
            if (currentUserId == null) return BadRequest("UserId missing");
            var response = await _userService.GetUserDetail((int)currentUserId);
            if (!response.Users.Any()) return NoContent();
            return Ok(response);
        }

        [Authorize(Roles = "create_user")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (request.Validate().Any()) return BadRequest("Invalid request");
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

        [Authorize(Roles = "create_user")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserRequest request)
        {
            if (request.Validate().Any()) return BadRequest("Invalid request");

            await _userService.RegisterUser(request);
            return Ok("User registered successfully");
        }

        [Authorize(Roles = "edit_user")]
        [HttpPatch]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            var currentUserId = Helper.GetRequestUserId(HttpContext);
            if (currentUserId == null) return BadRequest("UserId missing");
            if (request.Validate().Any()) return BadRequest("Invalid request");
            if (request.UserId != currentUserId) return BadRequest("UserId must match");
            try
            {
                await _userService.UpdateUser(request);
                return Ok("User updated successfully");
            }
            catch
            {
                return BadRequest("User update failed");
            }
        }

        [Authorize(Roles = "delete_user")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> RemoveUser([FromRoute] int userId)
        {
            try
            {
                var currentUserId = Helper.GetRequestUserId(HttpContext);
                if (currentUserId == null) return BadRequest("UserId missing");
                if (userId != currentUserId) return BadRequest("UserId must match");
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
