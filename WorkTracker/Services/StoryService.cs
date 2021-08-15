using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Models;
using WorkTracker.Models.Requests;
using WorkTracker.Repositories.Interfaces;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Services
{
    public class StoryService : IStoryService
    {
		private readonly IStoryRepository _storyRepository;
		private readonly IServiceLogRepository _serviceLogRepository;

		public StoryService(IStoryRepository storyRepository,
						    IServiceLogRepository serviceLogRepository)
		{
			_storyRepository = storyRepository;
			_serviceLogRepository = serviceLogRepository;
		}

		public async Task<List<Models.DTOs.Story>> GetStoriesByStateId(int userId, int stateId, bool getArchived)
        {
			return Mapper.Map(await _storyRepository.GetStoriesByStateId(userId, stateId, getArchived));
		}

		public async Task CreateStory(int userId, CreateStoryRequest request)
        {
			var story = Mapper.Map(request);
			var storyId = await _storyRepository.CreateStory(userId, story);
			var tasks = Mapper.Map(request, storyId);
			if (tasks != null)
            {
				await _storyRepository.AddTasks(tasks);
			}
			await _serviceLogRepository.Add(new Models.DataModels.ServiceLog
			{
				UserId = userId,
				CountAffected = 1,
				FunctionName = "CreateStory"
			});
		}

		public async Task UpdateStory(UpdateStoryRequest request, int userId)
        {
			var (story, tasks) = Mapper.Map(request);
			await _storyRepository.UpdateStory(story, userId);
			await _storyRepository.UpdateTasks(tasks);
			await _serviceLogRepository.Add(new Models.DataModels.ServiceLog
			{
				UserId = userId,
				CountAffected = 1,
				FunctionName = "UpdateStory"
			});
		}

		public async Task DeleteStory(int storyId, int userId)
        {
			await _storyRepository.DeleteStory(storyId, userId);
			await _serviceLogRepository.Add(new Models.DataModels.ServiceLog
			{
				UserId = userId,
				CountAffected = 1,
				FunctionName = "DeleteStory"
			});
		}

		public async Task OrderUpdate(int userId, OrderUpdateRequest request)
        {
			await _storyRepository.OrderUpdate(request.StateId, userId, request.Stories);
        }

		public async Task<List<Models.DTOs.Task>> GetStoryTasks(int storyId, int userId)
        {
			var tasks = await _storyRepository.GetStoryTasks(storyId, userId);
			return Mapper.Map(tasks);
        }

		public async Task DeleteTask(int taskId, int userId)
        {
			await _storyRepository.DeleteTask(taskId, userId);
			await _serviceLogRepository.Add(new Models.DataModels.ServiceLog
			{
				UserId = userId,
				CountAffected = 1,
				FunctionName = "DeleteTask"
			});
		}

		public async Task ChangeState(int userId, int storyId, OrderUpdateRequest request)
		{
			await _storyRepository.ChangeState(userId, storyId, request.StateId);
			await _storyRepository.OrderUpdate(request.StateId, userId, request.Stories);
			await _serviceLogRepository.Add(new Models.DataModels.ServiceLog
			{
				UserId = userId,
				CountAffected = 1,
				FunctionName = "ChangeState"
			});
		}
	}
}
