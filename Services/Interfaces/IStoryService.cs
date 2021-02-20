using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Models.Requests;
using WorkTracker.Models.ServiceModels;

namespace WorkTracker.Services.Interfaces
{
    public interface IStoryService
    {
        List<Models.DTOs.Story> GetStoriesByStateId(int userId, int stateId);
        void CreateStory(int userId, CreateStoryRequest request);
    }
}
