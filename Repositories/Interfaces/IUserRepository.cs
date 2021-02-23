using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Models.DataModels;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<Models.ServiceModels.User>> GetUsersByTeamId(int teamId);
        Task<Models.ServiceModels.User> GetUser(int userId);
        Task<int> CreateUser(Models.ServiceModels.User user);
        System.Threading.Tasks.Task UpdateUser(Models.ServiceModels.User user);
        System.Threading.Tasks.Task DeleteUser(int userId);
    }
}
