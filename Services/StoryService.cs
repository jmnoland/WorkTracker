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

		public async Task<List<Models.DTOs.Story>> GetStoriesByStateId(int userId, int stateId, bool getArchived)
        {
			return Mapper.Map(await _storyRepository.GetStoriesByStateId(userId, stateId, getArchived));
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

		public async System.Threading.Tasks.Task UpdateStory(UpdateStoryRequest request, int userId)
        {
			var (story, tasks) = Mapper.Map(request);
			await _storyRepository.UpdateStory(story, userId);
			await _storyRepository.UpdateTasks(tasks);
        }

		public async System.Threading.Tasks.Task DeleteStory(int storyId, int userId)
        {
			await _storyRepository.DeleteStory(storyId, userId);
        }

		public async System.Threading.Tasks.Task OrderUpdate(int userId, OrderUpdateRequest request)
        {
			await _storyRepository.OrderUpdate(request.StateId, userId, request.Stories);
        }

		public async Task<List<Models.DTOs.Task>> GetStoryTasks(int storyId, int userId)
        {
			var tasks = await _storyRepository.GetStoryTasks(storyId, userId);
			return Mapper.Map(tasks);
        }

		public async System.Threading.Tasks.Task DeleteTask(int taskId, int userId)
        {
			await _storyRepository.DeleteTask(taskId, userId);
        }

		public async System.Threading.Tasks.Task ChangeState(int userId, int storyId, OrderUpdateRequest request)
		{
			await _storyRepository.ChangeState(userId, storyId, request.StateId);
			await _storyRepository.OrderUpdate(request.StateId, userId, request.Stories);
		}
	}
}
