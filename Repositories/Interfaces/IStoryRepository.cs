using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Models.DataModels;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IStoryRepository
    {
        List<Models.ServiceModels.Story> GetStoriesByStateId(int userId, int stateId);
        int CreateStory(int userId, Models.ServiceModels.Story story);
        void AddTasks(List<Models.ServiceModels.Task> taskList);
    }
}
