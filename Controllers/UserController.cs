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
        public async Task<ActionResult<List<User>>> GetUsers([FromRoute] int teamId)
        {
            return await _userService.GetUsersByTeamId(teamId);
        }

        [HttpGet("details/{userId}")]
        public async Task<ActionResult<UserDetail>> GetUserDetails([FromRoute] int userId)
        {
            return await _userService.GetUserDetail(userId);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
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

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] CreateUserRequest request)
        {
            await _userService.RegisterUser(request);
            return Ok();
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            try
            {
                await _userService.UpdateUser(request);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{userId}")]
        public async Task<ActionResult> RemoveUser([FromRoute] int userId)
        {
            try
            {
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
