using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Models.Mapper;
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

		public List<Story> GetStoriesByStateId(int userId, int stateId)
        {
			return _storyRepository.GetStoriesByStateId(userId, stateId);
		}
	}
}
