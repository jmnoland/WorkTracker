using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkTracker.Models.Requests;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize(Roles = "create_project")]
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            var teamId = Helper.GetRequestTeamIds(HttpContext);
            if (userId == null) return BadRequest("User id missing");
            if (teamId == null) return BadRequest("Team id missing");
            request.TeamId = teamId.Value;
            
            return Ok(await _projectService.CreateProject(request));
        }
        
        [Authorize(Roles = "update_project")]
        [HttpPatch]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");

            return Ok(await _projectService.UpdateProject(request));
        }
        
        [Authorize(Roles = "view_project")]
        [HttpGet]
        public async Task<IActionResult> GetProjectByTeamId()
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");

            return Ok(await _projectService.GetByUserId(userId.Value));
        }
        
        [Authorize(Roles = "update_project")]
        [HttpPost("delete/{projectId}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int projectId)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            var teamId = Helper.GetRequestTeamIds(HttpContext);
            if (userId == null) return BadRequest("User id missing");
            if (teamId == null) return BadRequest("Team id missing");

            await _projectService.DeleteProject(projectId, teamId.Value);
            return Ok();
        }
        
        [Authorize(Roles = "update_project")]
        [HttpPost("complete/{projectId}")]
        public async Task<IActionResult> CompleteProject([FromRoute] int projectId)
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            var teamId = Helper.GetRequestTeamIds(HttpContext);
            if (userId == null) return BadRequest("User id missing");
            if (teamId == null) return BadRequest("Team id missing");

            await _projectService.CompleteProject(projectId, teamId.Value);
            return Ok();
        }
    }
}