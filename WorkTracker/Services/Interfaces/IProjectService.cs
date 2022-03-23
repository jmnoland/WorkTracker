using System.Collections.Generic; 
using System.Threading.Tasks;
using WorkTracker.Models.DTOs;
using WorkTracker.Models.Requests;

namespace WorkTracker.Services.Interfaces
{
    public interface IProjectService
    {
        Task<Dictionary<int, List<Project>>> GetByUserId(int userId);
        System.Threading.Tasks.Task CompleteProject(int projectId, int teamId);
        System.Threading.Tasks.Task DeleteProject(int projectId, int teamId);
        Task<Project> UpdateProject(UpdateProjectRequest request);
        Task<Project> CreateProject(CreateProjectRequest request);
    }
}