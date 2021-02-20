using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Models.DataModels;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IStoryRepository
    {
        List<Models.ServiceModels.Story> GetStoriesByStateId(int userId, int stateId);
    }
}
