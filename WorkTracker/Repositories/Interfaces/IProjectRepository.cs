using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Models.DataModels;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Models.ServiceModels.Project>> GetByTeamId(int teamId);
        Task<Models.ServiceModels.Project> CreateProject(Models.ServiceModels.Project project);
        Task<Models.ServiceModels.Project> UpdateProject(Models.ServiceModels.Project project);
        Task<Models.ServiceModels.Project> CompleteProject(int projectId, int teamId);
        Task<Models.ServiceModels.Project> DeleteProject(int projectId, int teamId);
        Task<Models.ServiceModels.Project> Find(int projectId, int teamId);
    }
}