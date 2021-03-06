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

        public async Task<List<Models.ServiceModels.Story>> GetStoriesByStateId(int userId, int stateId, bool getArchived)
        {
            var userStories = new List<Story>();
            if (getArchived)
            {
                userStories = await (from us in _dbContext.UserStory
                                         join story in _dbContext.Story on us.StoryId equals story.StoryId
                                         where us.UserId == userId && story.StateId == stateId
                                         select story).OrderBy(o => o.ListOrder).ToListAsync();
                return Mapper.Map(userStories);
            }
            userStories = await (from us in _dbContext.UserStory
                                        join story in _dbContext.Story on us.StoryId equals story.StoryId
                                        where us.UserId == userId
                                            && story.StateId == stateId
                                            && story.Archived != true
                                        select story).OrderBy(o => o.ListOrder).ToListAsync();
            return Mapper.Map(userStories);
        }
        
        public async Task<int> CreateStory(int userId, Models.ServiceModels.Story story)
        {
            var storyToAdd = Mapper.Map(story);
            storyToAdd.CreatedAt = DateTime.Now;
            storyToAdd.CreatedBy = userId;
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

        public async System.Threading.Tasks.Task UpdateStory(Models.ServiceModels.Story updatedStory)
        {
            var story = await _dbContext.Story
                .Where(w => w.StoryId == updatedStory.StoryId)
                .FirstOrDefaultAsync();
            story.ProjectId = updatedStory.ProjectId ?? story.ProjectId;
            story.SprintId = updatedStory.SprintId ?? story.ProjectId;
            story.StateId = updatedStory.StateId;
            story.Title = updatedStory.Title ?? story.Title;
            story.Description = updatedStory.Description ?? story.Description;
            story.ModifiedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();
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

        public async System.Threading.Tasks.Task UpdateTasks(List<Models.ServiceModels.Task> taskList)
        {
            var editList = Mapper.Map(taskList);
            List<int> idList = new List<int>();
            foreach (var item in editList) idList.Add(item.TaskId);
            var existingTasks = _dbContext.Task.Where(w => idList.Contains(w.TaskId));
            foreach(var task in existingTasks)
            {
                var updatedTask = editList.Find(f => f.TaskId == task.TaskId);
                task.Complete = updatedTask.Complete;
                task.Description = updatedTask.Description ?? task.Description;
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
