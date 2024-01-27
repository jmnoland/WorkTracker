using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTracker.Models.DataModels;
using WorkTracker.Repositories.Interfaces;

namespace WorkTracker.Repositories
{
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        private readonly WorkTrackerContext _dbContext;

        public StateRepository(WorkTrackerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<State>> CreateDefaultStates(int teamId)
        {
            var states = new List<State>
            {
                new() { StateId = 0, TeamId = teamId, Name = "New", Type = "New"},
                new() { StateId = 0, TeamId = teamId, Name = "In progress", Type = "In progress"},
                new() { StateId = 0, TeamId = teamId, Name = "Complete", Type = "Complete"},
            };
            _dbContext.States.AddRange(states);
            await _dbContext.SaveChangesAsync();
            return states;
        }

        public async Task<List<Models.ServiceModels.State>> GetByTeamId(int teamId)
        {
            return Mapper.Map(await _dbContext.States.Where(w => w.TeamId == teamId).ToListAsync());
        }

    }
}
