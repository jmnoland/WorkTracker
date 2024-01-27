using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Models.DataModels;

namespace WorkTracker.Repositories.Interfaces
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        System.Threading.Tasks.Task AssignUser(int userId, int teamId);
        Task<List<Models.ServiceModels.Team>> GetByUserId(int userId);
    }
}
