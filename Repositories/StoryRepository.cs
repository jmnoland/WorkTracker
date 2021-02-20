using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkTracker.Models.DataModels;
using WorkTracker.Repositories.Interfaces;

namespace WorkTracker.Repositories
{
    public class StoryRepository : GenericRepository<Story>, IStoryRepository
    {
        private readonly WorkTrackerContext _dbContext;

        public StoryRepository(WorkTrackerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Models.ServiceModels.Story> GetStoriesByStateId(int userId, int stateId)
        {
            var userStories = (from us in _dbContext.UserStory
                               join story in _dbContext.Story on us.StoryId equals story.StoryId
                               where us.UserId == userId && story.StateId == stateId
                               select story).ToList();
            return Mapper.Map(userStories);
        }
    }
}
