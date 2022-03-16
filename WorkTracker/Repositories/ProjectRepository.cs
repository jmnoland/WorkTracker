using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkTracker.Models.DataModels;
using WorkTracker.Repositories.Interfaces;

namespace WorkTracker.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly WorkTrackerContext _dbContext;

        public ProjectRepository(WorkTrackerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Models.ServiceModels.Project>> GetByTeamId(int teamId)
        {
            var data = await _dbContext.Projects
                .Where(w => w.TeamId == teamId && w.DeletedAt != null)
                .ToListAsync();
            return Mapper.Map(data);
        }
    }
}
