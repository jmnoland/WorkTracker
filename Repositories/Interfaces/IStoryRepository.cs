using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Models.DataModels;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IStoryRepository
    {
        Task<List<Models.ServiceModels.Story>> GetStoriesByStateId(int userId, int stateId, bool getArchived);
        Task<int> CreateStory(int userId, Models.ServiceModels.Story story);
        System.Threading.Tasks.Task UpdateStory(Models.ServiceModels.Story updatedStory, int userId);
        System.Threading.Tasks.Task DeleteStory(int storyId, int userId);
        System.Threading.Tasks.Task OrderUpdate(int stateId, int userId, Dictionary<string, int> updateList);
        System.Threading.Tasks.Task ChangeState(int userId, int storyId, int stateId);
        Task<List<Models.ServiceModels.Task>> GetStoryTasks(int storyId, int userId);
        System.Threading.Tasks.Task AddTasks(List<Models.ServiceModels.Task> taskList);
        System.Threading.Tasks.Task UpdateTasks(List<Models.ServiceModels.Task> taskList);
        System.Threading.Tasks.Task DeleteTask(int taskId, int userId);
    }
}
