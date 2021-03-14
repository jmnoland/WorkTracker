using Microsoft.AspNetCore.Mvc;
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
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _storyService;
        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        [ValidateToken("view_story")]
        [HttpGet("{stateId}")]
        public async Task<ActionResult<List<Story>>> GetStoriesByStateId([FromRoute] int stateId)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest();

            return Ok(await _storyService.GetStoriesByStateId((int)userId, stateId, false));
        }

        [ValidateToken("view_story")]
        [HttpGet("archived/{stateId}")]
        public async Task<ActionResult<List<Story>>> GetArchivedStories([FromRoute] int stateId)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest();

            return Ok(await _storyService.GetStoriesByStateId((int)userId, stateId, true));
        }

        [ValidateToken("create_story")]
        [HttpPost]
        public async Task<ActionResult> CreateStory([FromBody] CreateStoryRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null || !ModelState.IsValid) return BadRequest();

            await _storyService.CreateStory((int)userId, request);
            return Ok();
        }

        [ValidateToken("edit_story")]
        [HttpPatch]
        public async Task<ActionResult> UpdateStory([FromBody] UpdateStoryRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _storyService.UpdateStory(request);
            return Ok();
        }

        [ValidateToken("edit_story")]
        [HttpDelete("{storyId}")]
        public async Task<ActionResult> DeleteStory([FromRoute] int storyId)
        {
            await _storyService.DeleteStory(storyId);
            return Ok();
        }

        [ValidateToken("view_story")]
        [HttpGet("task/{storyId}")]
        public async Task<ActionResult<List<Story>>> GetStoryTasks([FromRoute] int storyId)
        {
            return Ok(await _storyService.GetStoryTasks(storyId));
        }

        [ValidateToken("edit_story")]
        [HttpDelete("task/{taskId}")]
        public async Task<ActionResult> DeleteTask([FromRoute] int taskId)
        {
            await _storyService.DeleteTask(taskId);
            return Ok();
        }

        [ValidateToken("view_story")]
        [HttpPatch("update/state/{storyId}")]
        public async Task<ActionResult> ChangeState([FromRoute] int storyId, [FromBody] OrderUpdateRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null || !ModelState.IsValid) return BadRequest();

            await _storyService.ChangeState((int)userId, storyId, request);
            return Ok();
        }

        [ValidateToken("view_story")]
        [HttpPatch("update/order")]
        public async Task<ActionResult> OrderUpdate([FromBody] OrderUpdateRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null || !ModelState.IsValid) return BadRequest();

            await _storyService.OrderUpdate((int)userId, request);
            return Ok();
        }
    }
}
