using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Models.Requests;

namespace WorkTracker.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<Models.DTOs.User>> GetAllUsers(int teamId);
        Task<List<Models.DTOs.User>> GetUsersByTeamId(int teamId);
        Task<Models.DTOs.UserDetail> GetUserDetail(int userId);
        Task CreateUser(CreateUserRequest request);
        Task RegisterUser(CreateUserRequest request);
        Task<Models.DTOs.User> UpdateUser(UpdateUserRequest request);
        Task DeleteUser(int userId);
    }
}
