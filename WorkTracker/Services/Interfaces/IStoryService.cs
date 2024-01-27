using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Models.Requests;

namespace WorkTracker.Services.Interfaces
{
    public interface IStoryService
    {
        Task<List<Models.DTOs.Story>> GetStoriesByStateId(int userId, int stateId, bool getArchived);
        System.Threading.Tasks.Task CreateStory(int userId, CreateStoryRequest request);
        System.Threading.Tasks.Task UpdateStory(UpdateStoryRequest request, int userId);
        System.Threading.Tasks.Task DeleteStory(int storyId, int userId);
        System.Threading.Tasks.Task OrderUpdate(int userId, OrderUpdateRequest request);
        Task<List<Models.DTOs.Task>> GetStoryTasks(int storyId, int userId);
        System.Threading.Tasks.Task ChangeState(int userId, int storyId, OrderUpdateRequest request);
        System.Threading.Tasks.Task DeleteTask(int taskId, int userId);
    }
}
