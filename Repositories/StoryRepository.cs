﻿using Microsoft.EntityFrameworkCore;
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
                               select story).ToListAsync();
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
        public async System.Threading.Tasks.Task AddTasks(List<Models.ServiceModels.Task> taskList)
        {
            var addList = Mapper.Map(taskList);
            _dbContext.AddRange(addList);
            await _dbContext.SaveChangesAsync();
        }
    }
}