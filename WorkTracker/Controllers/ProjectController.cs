using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public async Task<IActionResult> CreateProject()
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");

            return Ok(await _projectService.GetByUserId(userId.Value));
        }
        
        [Authorize(Roles = "update_project")]
        [HttpGet]
        public async Task<IActionResult> UpdateProject()
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");

            return Ok(await _projectService.GetByUserId(userId.Value));
        }
        
        [Authorize(Roles = "view_project")]
        [HttpGet]
        public async Task<IActionResult> GetProjectByTeamId()
        {
            var userId = Helper.GetRequestUserId(HttpContext);
            if (userId == null) return BadRequest("User id missing");

            return Ok(await _projectService.GetByUserId(userId.Value));
        }
    }
}