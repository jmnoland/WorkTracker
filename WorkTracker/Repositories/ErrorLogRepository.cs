using WorkTracker.Models.DataModels;
using WorkTracker.Repositories.Interfaces;

namespace WorkTracker.Repositories
{
    public class ErrorLogRepository : GenericRepository<ErrorLog>, IErrorLogRepository
    {
        private readonly WorkTrackerContext _dbContext;

        public ErrorLogRepository(WorkTrackerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
