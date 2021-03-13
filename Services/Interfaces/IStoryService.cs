﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Models.Requests;
using WorkTracker.Models.ServiceModels;

namespace WorkTracker.Services.Interfaces
{
    public interface IStoryService
    {
        Task<List<Models.DTOs.Story>> GetStoriesByStateId(int userId, int stateId, bool getArchived);
        System.Threading.Tasks.Task CreateStory(int userId, CreateStoryRequest request);
        System.Threading.Tasks.Task UpdateStory(UpdateStoryRequest request);
        System.Threading.Tasks.Task DeleteStory(int storyId);
        System.Threading.Tasks.Task OrderUpdate(int userId, OrderUpdateRequest request);
        System.Threading.Tasks.Task ChangeState(int userId, int storyId, OrderUpdateRequest request);
        System.Threading.Tasks.Task DeleteTask(int taskId);
    }
}
