using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public ActionResult<List<Story>> GetStoriesByStateId([FromRoute] int stateId)
        {
            var userId = Helper.GetRequestUserId(HttpContext);

            return _storyService.GetStoriesByStateId(userId, stateId);
        }

        [ValidateToken("create_story")]
        [HttpPost]
        public ActionResult<List<Story>> CreateStory([FromBody] CreateStoryRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);

            _storyService.CreateStory(userId, request);
            return Ok();
        }
    }
}
