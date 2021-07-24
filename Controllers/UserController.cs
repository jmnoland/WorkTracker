using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "view_user")]
        [HttpGet("{teamId}")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var currentUserId = Helper.GetRequestUserId(HttpContext);
            if (currentUserId == null) return BadRequest("UserId missing");
            var userList = await _userService.GetUsersByTeamId((int)currentUserId);
            return userList;
        }

        [Authorize(Roles = "view_user")]
        [HttpGet("details")]
        public async Task<ActionResult<UserDetail>> GetUserDetails()
        {
            var currentUserId = Helper.GetRequestUserId(HttpContext);
            if (currentUserId == null) return BadRequest("UserId missing");
            var response = await _userService.GetUserDetail((int)currentUserId);
            if (response == null) return NoContent();
            return Ok(response);
        }

        [Authorize(Roles = "create_user")]
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                await _userService.CreateUser(request);
                return Ok();
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Role does not exist.");
            }
        }

        [Authorize(Roles = "create_user")]
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _userService.RegisterUser(request);
            return Ok();
        }

        [Authorize(Roles = "edit_user")]
        [HttpPatch]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            var currentUserId = Helper.GetRequestUserId(HttpContext);
            if (currentUserId == null) return BadRequest("UserId missing");

            try
            {
                await _userService.UpdateUser(request);
                return Ok();
            }
            catch
            {
                throw new Exception("Update failed");
            }
        }

        [Authorize(Roles = "delete_user")]
        [HttpDelete("{userId}")]
        public async Task<ActionResult> RemoveUser([FromRoute] int userId)
        {
            try
            {
                var currentUserId = Helper.GetRequestUserId(HttpContext);
                if (currentUserId == null) return BadRequest("UserId missing");
                if (userId != currentUserId) return BadRequest("UserId must match");
                await _userService.DeleteUser(userId);
                return Ok();
            }
            catch
            {
                return BadRequest("Unable to remove user.");
            }
        }
    }
}
