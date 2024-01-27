using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Models.DataModels;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IStateRepository
    {
        Task<List<State>> CreateDefaultStates(int teamId);
        Task<List<Models.ServiceModels.State>> GetByTeamId(int teamId);
    }
}
