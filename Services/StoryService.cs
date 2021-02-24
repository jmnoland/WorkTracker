﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Models.Mapper;
using WorkTracker.Models.Requests;
using WorkTracker.Models.ServiceModels;
using WorkTracker.Repositories.Interfaces;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Services
{
    public class StoryService : IStoryService
    {
		private readonly IStoryRepository _storyRepository;
		public StoryService(IStoryRepository storyRepository)
		{
			_storyRepository = storyRepository;
		}

		public async Task<List<Models.DTOs.Story>> GetStoriesByStateId(int userId, int stateId)
        {
			return Mapper.Map(await _storyRepository.GetStoriesByStateId(userId, stateId));
		}

		public async System.Threading.Tasks.Task CreateStory(int userId, CreateStoryRequest request)
        {
			var story = Mapper.Map(request);
			var storyId = await _storyRepository.CreateStory(userId, story);
			var tasks = Mapper.Map(request, storyId);
			if (tasks != null)
            {
				await _storyRepository.AddTasks(tasks);
			}
		}
	}
}