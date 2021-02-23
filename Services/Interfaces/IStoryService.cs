using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Models.Requests;
using WorkTracker.Models.ServiceModels;

namespace WorkTracker.Services.Interfaces
{
    public interface IStoryService
    {
        Task<List<Models.DTOs.Story>> GetStoriesByStateId(int userId, int stateId);
        System.Threading.Tasks.Task CreateStory(int userId, CreateStoryRequest request);
    }
}
