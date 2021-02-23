using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Models.DataModels;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IStoryRepository
    {
        Task<List<Models.ServiceModels.Story>> GetStoriesByStateId(int userId, int stateId);
        Task<int> CreateStory(int userId, Models.ServiceModels.Story story);
        System.Threading.Tasks.Task AddTasks(List<Models.ServiceModels.Task> taskList);
    }
}
