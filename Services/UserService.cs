using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Repositories.Interfaces;
using WorkTracker.Models.Mapper;
using WorkTracker.Models.ServiceModels;
using WorkTracker.Services.Interfaces;
using WorkTracker.Models.Requests;

namespace WorkTracker.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserService(IUserRepository userRepository,
                           IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public List<Models.DTOs.User> GetUsers(int teamId)
        {
            return Mapper.Map(_userRepository.GetUsers(teamId));
        }

        public void CreateUser(CreateUserRequest request)
        {
            var user = Mapper.Map(request);
            _userRepository.CreateUser(user);
        }

        public void UpdateUser(UpdateUserRequest request)
        {
            var user = _userRepository.GetUser(request.UserId);
            _userRepository.UpdateUser(Mapper.Map(request, user));
        }

        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
        }
    }
}
