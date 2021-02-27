using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<List<Models.ServiceModels.Story>> GetStoriesByStateId(int userId, int stateId)
        {
            var userStories = await (from us in _dbContext.UserStory
                               join story in _dbContext.Story on us.StoryId equals story.StoryId
                               where us.UserId == userId && story.StateId == stateId
                               select story).OrderBy(o => o.ListOrder).ToListAsync();
            return Mapper.Map(userStories);
        }
        
        public async Task<int> CreateStory(int userId, Models.ServiceModels.Story story)
        {
            var storyToAdd = Mapper.Map(story);
            _dbContext.Story.Add(storyToAdd);
            await _dbContext.SaveChangesAsync();
            var userStory = new UserStory
            {
                StoryId = storyToAdd.StoryId,
                UserId = userId
            };
            _dbContext.UserStory.Add(userStory);
            await _dbContext.SaveChangesAsync();
            return storyToAdd.StoryId;
        }

        public async System.Threading.Tasks.Task OrderUpdate(int stateId, int userId, Dictionary<string, int> updateList)
        {
            var stories = await (from s in _dbContext.Story
                                 join ut in _dbContext.UserStory on s.StoryId equals ut.StoryId
                                 where ut.UserId == userId && s.StateId == stateId
                                 select s).ToListAsync();
            foreach(var story in stories)
            {
                if (updateList.TryGetValue(story.StoryId.ToString(), out int newOrderNum))
                {
                    story.ListOrder = newOrderNum;
                }
            }
            await _dbContext.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task ChangeState(int userId, int storyId, int stateId)
        {
            var story = await (from s in _dbContext.Story
                                 join ut in _dbContext.UserStory on s.StoryId equals ut.StoryId
                                 where ut.UserId == userId && s.StoryId == storyId
                                 select s).FirstOrDefaultAsync();
            story.StateId = stateId;
            await _dbContext.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddTasks(List<Models.ServiceModels.Task> taskList)
        {
            var addList = Mapper.Map(taskList);
            _dbContext.AddRange(addList);
            await _dbContext.SaveChangesAsync();
        }
    }
}
