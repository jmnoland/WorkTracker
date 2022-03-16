using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Models.ServiceModels.Project>> GetByTeamId(int teamId);
    }
}