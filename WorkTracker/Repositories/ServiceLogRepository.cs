using WorkTracker.Models.DataModels;
using WorkTracker.Repositories.Interfaces;

namespace WorkTracker.Repositories
{
    public class ServiceLogRepository : GenericRepository<ServiceLog>, IServiceLogRepository
    {
        private readonly WorkTrackerContext _dbContext;

        public ServiceLogRepository(WorkTrackerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
