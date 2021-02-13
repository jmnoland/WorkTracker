using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Models.DataModels;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        List<Models.ServiceModels.User> GetUsersByTeamId(int teamId);
        User GetUser(int userId);
        int CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
    }
}
