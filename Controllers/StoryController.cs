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
        private readonly int userId;
        public StoryController(IStoryService storyService)
        {
            userId = Helper.GetRequestUserId(HttpContext);
            _storyService = storyService;
        }

        [ValidateToken("view_story")]
        [HttpGet("{stateId}")]
        public async Task<ActionResult<List<Story>>> GetStoriesByStateId([FromRoute] int stateId)
        {
            return Ok(await _storyService.GetStoriesByStateId(userId, stateId));
        }

        [ValidateToken("create_story")]
        [HttpPost]
        public async Task<ActionResult> CreateStory([FromBody] CreateStoryRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            await _storyService.CreateStory(userId, request);
            return Ok();
        }

        [ValidateToken("view_story")]
        [HttpPatch("update/state/{storyId}")]
        public async Task<ActionResult> ChangeState([FromRoute] int storyId, [FromBody] OrderUpdateRequest request)
        {
            await _storyService.ChangeState(userId, storyId, request);
            return Ok();
        }

        [ValidateToken("view_story")]
        [HttpPatch("update/order")]
        public async Task<ActionResult> OrderUpdate([FromBody] OrderUpdateRequest request)
        {
            await _storyService.OrderUpdate(userId, request);
            return Ok();
        }

    }
}
