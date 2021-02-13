using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Models.Requests;

namespace WorkTracker.Services.Interfaces
{
    public interface IUserService
    {
        List<Models.DTOs.User> GetUsersByTeamId(int teamId);
        Models.DTOs.UserDetail GetUserDetail(int userId);
        void CreateUser(CreateUserRequest request);
        void RegisterUser(CreateUserRequest request);
        void UpdateUser(UpdateUserRequest request);
        void DeleteUser(int userId);
    }
}
