using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Models.DataModels;

namespace WorkTracker.Repositories.Interfaces
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        System.Threading.Tasks.Task AssignUser(int userId, int teamId);
        List<Models.ServiceModels.Team> GetByUserId(int userId);
    }
}
