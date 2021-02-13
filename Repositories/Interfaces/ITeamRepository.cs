using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Models.DataModels;

namespace WorkTracker.Repositories.Interfaces
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        void AssignUser(int userId, int teamId);
    }
}
