using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async System.Threading.Tasks.Task UpdateStory(Models.ServiceModels.Story updatedStory, int userId)
        {
            var story = await (from s in _dbContext.Story
                              join us in _dbContext.UserStory on s.StoryId equals us.StoryId
                              where s.StoryId == updatedStory.StoryId && us.UserId == userId
                              select s).FirstOrDefaultAsync();
            if (story == null) throw new Exception("Update failed story not found");
            story.ProjectId = updatedStory.ProjectId ?? story.ProjectId;
            story.SprintId = updatedStory.SprintId ?? story.ProjectId;
            story.StateId = updatedStory.StateId;
            story.Title = updatedStory.Title ?? story.Title;
            story.Description = updatedStory.Description ?? story.Description;
            story.ModifiedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteStory(int storyId, int userId)
        {
            var story = await (from s in _dbContext.Story
                               join us in _dbContext.UserStory on s.StoryId equals us.StoryId
                               where s.StoryId == storyId && us.UserId == userId
                               select s).AnyAsync();
            if (!story) throw new Exception("Delete failed story not found");;
            const string query = @"
                DELETE FROM Story WHERE StoryId = @storyId
                DELETE FROM UserStory WHERE StoryId = @storyId
                DELETE FROM Task WHERE StoryId = @storyId
            ";

            var conn = (SqlConnection)_dbContext.Database.GetDbConnection();
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@storyId", storyId);
                await cmd.ExecuteNonQueryAsync();
                await conn.CloseAsync();
            }
        }

        public async System.Threading.Tasks.Task OrderUpdate(int stateId, int userId, Dictionary<string, int> updateList)
        {
            var stories = await (from s in _dbContext.Story
                                 join ut in _dbContext.UserStory on s.StoryId equals ut.StoryId
                                 where ut.UserId == userId && s.StateId == stateId
                                 select s).ToListAsync();
            if (stories.Count() >= 0)
            {
                foreach(var story in stories)
                {
                    if (updateList.TryGetValue(story.StoryId.ToString(), out var newOrderNum))
                    {
                        story.ListOrder = newOrderNum;
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Order update failed stories not found");
            }
        }

        public async System.Threading.Tasks.Task ChangeState(int userId, int storyId, int stateId)
        {
            var story = await (from s in _dbContext.Story
                                 join ut in _dbContext.UserStory on s.StoryId equals ut.StoryId
                                 where ut.UserId == userId && s.StoryId == storyId
                                 select s).FirstOrDefaultAsync();
            if (story == null) throw new Exception("Change state failed story not found");
            story.StateId = stateId;
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task<List<Models.ServiceModels.Task>> GetStoryTasks(int storyId, int userId)
        {
            var story = await (from s in _dbContext.Story
                               join us in _dbContext.UserStory on s.StoryId equals us.StoryId
                               where s.StoryId == storyId && us.UserId == userId
                               select s).AnyAsync();
            if (!story) throw new Exception($"Task not found for user {userId}");

            var tasks = await _dbContext.Task.Where(w => w.StoryId == storyId).ToListAsync();
            return Mapper.Map(tasks);
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
            var idList = editList.Select(s => s.TaskId).ToList();
            var existingTasks = _dbContext.Task.Where(w => idList.Contains(w.TaskId));
            foreach(var task in existingTasks)
            {
                var updatedTask = editList.Find(f => f.TaskId == task.TaskId);
                task.Complete = updatedTask.Complete;
                task.Description = updatedTask.Description ?? task.Description;
            }
            _dbContext.AddRange(editList.Where(w => w.TaskId == 0).ToList());
            await _dbContext.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteTask(int taskId, int userId)
        {
            var story = await (from s in _dbContext.Story
                               join us in _dbContext.UserStory on s.StoryId equals us.StoryId
                               join tas in _dbContext.Task on s.StoryId equals tas.StoryId
                               where tas.TaskId == taskId && us.UserId == userId
                               select s).AnyAsync();
            if (!story) throw new Exception(string.Format("Task not owned by user {0}", userId));

            var task = await _dbContext.Task.Where(w => w.TaskId == taskId).FirstOrDefaultAsync();
            if (task == null) return;
            _dbContext.Task.Remove(task);
            await _dbContext.SaveChangesAsync();
        }
    }
}
