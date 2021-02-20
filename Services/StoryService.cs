using System;
using System.Collections.Generic;
using System.Text;
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

		public List<Models.DTOs.Story> GetStoriesByStateId(int userId, int stateId)
        {
			return Mapper.Map(_storyRepository.GetStoriesByStateId(userId, stateId));
		}

		public void CreateStory(int userId, CreateStoryRequest request)
        {
			var story = Mapper.Map(request);
			var storyId = _storyRepository.CreateStory(userId, story);
			var tasks = Mapper.Map(request, storyId);
			if (tasks != null)
            {
				_storyRepository.AddTasks(tasks);
			}
		}
	}
}
