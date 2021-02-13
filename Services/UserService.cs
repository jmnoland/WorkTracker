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
        private readonly ITeamRepository _teamRepository;
        public UserService(IUserRepository userRepository,
                           IRoleRepository roleRepository,
                           ITeamRepository teamRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _teamRepository = teamRepository;
        }

        public List<Models.DTOs.User> GetUsersByTeamId(int teamId)
        {
            return Mapper.Map(_userRepository.GetUsersByTeamId(teamId));
        }

        public void CreateUser(CreateUserRequest request)
        {
            var user = Mapper.Map(request);
            if (user.RoleId == 0) user.RoleId = 1;
            _userRepository.CreateUser(user);
        }

        public void RegisterUser(CreateUserRequest request)
        {
            var user = Mapper.Map(request);
            if (user.RoleId == 0) user.RoleId = 1;
            var userId = _userRepository.CreateUser(user);
            var newTeam = new Models.DataModels.Team()
            {
                OrganisationId = null,
                TeamId = 0,
                Name = Guid.NewGuid().ToString()
            };
            var team = _teamRepository.Add(newTeam);
            _teamRepository.AssignUser(userId, team.TeamId);
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
