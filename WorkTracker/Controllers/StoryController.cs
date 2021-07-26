using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Models.DTOs;
using WorkTracker.Models.Requests;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _storyService;
        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        [Authorize(Roles = "view_story")]
        [HttpGet("{stateId}")]
        public async Task<IActionResult> GetStoriesByStateId([FromRoute] int stateId)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");

            return Ok(await _storyService.GetStoriesByStateId((int)userId, stateId, false));
        }

        [Authorize(Roles = "view_story")]
        [HttpGet("archived/{stateId}")]
        public async Task<IActionResult> GetArchivedStories([FromRoute] int stateId)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");

            return Ok(await _storyService.GetStoriesByStateId((int)userId, stateId, true));
        }

        [Authorize(Roles = "create_story")]
        [HttpPost]
        public async Task<IActionResult> CreateStory([FromBody] CreateStoryRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");
            if (request.Validate().Count > 0) return BadRequest("Request validation failed");

            await _storyService.CreateStory((int)userId, request);
            return Ok();
        }

        [Authorize(Roles = "edit_story")]
        [HttpPatch]
        public async Task<IActionResult> UpdateStory([FromBody] UpdateStoryRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");
            if (request.Validate().Count > 0) return BadRequest("Request validation failed");

            await _storyService.UpdateStory(request, (int)userId);
            return Ok();
        }

        [Authorize(Roles = "delete_story")]
        [HttpDelete("{storyId}")]
        public async Task<IActionResult> DeleteStory([FromRoute] int storyId)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");

            await _storyService.DeleteStory(storyId, (int)userId);
            return Ok();
        }

        [Authorize(Roles = "view_story")]
        [HttpGet("task/{storyId}")]
        public async Task<IActionResult> GetStoryTasks([FromRoute] int storyId)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");

            return Ok(await _storyService.GetStoryTasks(storyId, (int)userId));
        }

        [Authorize(Roles = "edit_story")]
        [HttpDelete("task/{taskId}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int taskId)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");

            await _storyService.DeleteTask(taskId, (int)userId);
            return Ok();
        }

        [Authorize(Roles = "view_story")]
        [HttpPatch("update/state/{storyId}")]
        public async Task<IActionResult> ChangeState([FromRoute] int storyId, [FromBody] OrderUpdateRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");
            if (request.Validate().Count > 0) return BadRequest("Request validation failed");

            await _storyService.ChangeState((int)userId, storyId, request);
            return Ok();
        }

        [Authorize(Roles = "view_story")]
        [HttpPatch("update/order")]
        public async Task<IActionResult> OrderUpdate([FromBody] OrderUpdateRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");
            if (request.Validate().Count > 0) return BadRequest("Request validation failed");

            await _storyService.OrderUpdate((int)userId, request);
            return Ok();
        }
    }
}
