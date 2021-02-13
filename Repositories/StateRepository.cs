using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void CreateDefaultStates(int teamId)
        {
            var states = new List<State>()
            {
                new State { StateId = 0, TeamId = teamId, Name = "New", Type = "New"},
                new State { StateId = 0, TeamId = teamId, Name = "In progress", Type = "In progress"},
                new State { StateId = 0, TeamId = teamId, Name = "Complete", Type = "Complete"},
            };
            _dbContext.States.AddRange(states);
            _dbContext.SaveChanges();
        }

        public List<State> GetByTeamId(int teamId)
        {
            return _dbContext.States.Where(w => w.TeamId == teamId).ToList();
        }

    }
}
