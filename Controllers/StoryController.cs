using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet("{stateId}")]
        public ActionResult<List<Story>> GetStoriesByStateId([FromRoute] int stateId)
        {
            return Ok();
        }
    }
}
