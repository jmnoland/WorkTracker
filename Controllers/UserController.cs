using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public ActionResult<List<User>> GetUsers([FromRoute] int teamId)
        {
            return _userService.GetUsersByTeamId(teamId);
        }

        [HttpGet("details")]
        public ActionResult<UserDetail> GetUserDetails([FromRoute] int userId)
        {
            return _userService.GetUserDetail(userId);
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                _userService.CreateUser(request);
                return Ok();
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Role does not exist.");
            }
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] CreateUserRequest request)
        {
            _userService.RegisterUser(request);
            return Ok();
        }

        [HttpPatch]
        public ActionResult UpdateUser([FromBody] UpdateUserRequest request)
        {
            try
            {
                _userService.UpdateUser(request);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{userId}")]
        public ActionResult RemoveUser([FromRoute] int userId)
        {
            try
            {
                _userService.DeleteUser(userId);
                return Ok();
            }
            catch
            {
                return BadRequest("Unable to remove user.");
            }
        }
    }
}
