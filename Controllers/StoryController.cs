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
            return Ok(await _storyService.GetStoriesByStateId(userId, stateId, false));
        }

        [ValidateToken("view_story")]
        [HttpGet("archived/{stateId}")]
        public async Task<ActionResult<List<Story>>> GetArchivedStories([FromRoute] int stateId)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            return Ok(await _storyService.GetStoriesByStateId(userId, stateId, true));
        }

        [ValidateToken("create_story")]
        [HttpPost]
        public async Task<ActionResult> CreateStory([FromBody] CreateStoryRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            await _storyService.CreateStory(userId, request);
            return Ok();
        }

        [ValidateToken("edit_story")]
        [HttpPatch]
        public async Task<ActionResult> UpdateStory([FromBody] UpdateStoryRequest request)
        {
            await _storyService.UpdateStory(request);
            return Ok();
        }

        [ValidateToken("view_story")]
        [HttpPatch("update/state/{storyId}")]
        public async Task<ActionResult> ChangeState([FromRoute] int storyId, [FromBody] OrderUpdateRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            await _storyService.ChangeState(userId, storyId, request);
            return Ok();
        }

        [ValidateToken("view_story")]
        [HttpPatch("update/order")]
        public async Task<ActionResult> OrderUpdate([FromBody] OrderUpdateRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            await _storyService.OrderUpdate(userId, request);
            return Ok();
        }
    }
}
