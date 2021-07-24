using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Repositories.Interfaces;
using WorkTracker.Models.Mapper;
using WorkTracker.Models.ServiceModels;
using WorkTracker.Services.Interfaces;
using WorkTracker.Models.Requests;
using System.Threading.Tasks;
using System.Linq;

namespace WorkTracker.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IServiceLogRepository _serviceLogRepository;
        public UserService(IUserRepository userRepository,
                           IRoleRepository roleRepository,
                           ITeamRepository teamRepository,
                           IStateRepository stateRepository,
                           IServiceLogRepository serviceLogRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _teamRepository = teamRepository;
            _stateRepository = stateRepository;
            _serviceLogRepository = serviceLogRepository;
        }

        public async Task<List<Models.DTOs.User>> GetAllUsers(int teamId)
        {
            return Mapper.Map(await _userRepository.GetAllUsers(teamId));
        }

        public async Task<List<Models.DTOs.User>> GetUsersByTeamId(int currentUserId)
        {
            var teams = _teamRepository.GetByUserId(currentUserId);
            var result = await _userRepository.GetUsersByTeamId(teams.Select(s => s.TeamId));
            if (result != null)
            {
                return Mapper.Map(result);
            }
            return null;
        }

        public async Task<Models.DTOs.UserDetail> GetUserDetail(int userId)
        {
            var teams = _teamRepository.GetByUserId(userId);
            if (teams.Count() > 0)
            {
                var details = new Models.DTOs.UserDetail()
                {
                    States = new List<Models.DTOs.State>(),
                    Teams = new List<Models.DTOs.Team>(),
                    Users = new List<Models.DTOs.User>()
                };
                details.Users.AddRange(Mapper.Map(await _userRepository.GetAllUsers(teams[0].TeamId)));
                foreach (var team in teams)
                {
                    details.States.AddRange(Mapper.Map(await _stateRepository.GetByTeamId(team.TeamId)));
                    details.Teams.Add(Mapper.Map(team));
                }
                return details;
            }
            return null;
        }

        public async System.Threading.Tasks.Task CreateUser(CreateUserRequest request)
        {
            var user = Mapper.Map(request);
            if (user.RoleId == 0) user.RoleId = 1;
            await _userRepository.CreateUser(user);
            await _serviceLogRepository.Add(new Models.DataModels.ServiceLog
            {
                UserId = user.UserId,
                FunctionName = "CreateUser",
            });
        }

        // To update when organisation/teams implemented fully
        // For registering user to an existing team
        public async System.Threading.Tasks.Task RegisterUser(CreateUserRequest request)
        {
            var user = Mapper.Map(request);
            if (user.RoleId == 0) user.RoleId = 1;
            var userId = await _userRepository.CreateUser(user);
            var newTeam = new Models.DataModels.Team()
            {
                OrganisationId = null,
                TeamId = 0,
                Name = Guid.NewGuid().ToString()
            };
            var team = await _teamRepository.Add(newTeam);
            await _teamRepository.AssignUser(userId, team.TeamId);
            await _stateRepository.CreateDefaultStates(team.TeamId);

            await _serviceLogRepository.Add(new Models.DataModels.ServiceLog
            {
                UserId = user.UserId,
                TeamId = team.TeamId,
                FunctionName = "RegisterUser",
            });
        }

        public async System.Threading.Tasks.Task UpdateUser(UpdateUserRequest request)
        {
            var user = await _userRepository.GetUser((int)request.UserId);
            await _userRepository.UpdateUser(Mapper.Map(request, user));

            await _serviceLogRepository.Add(new Models.DataModels.ServiceLog
            {
                UserId = user.UserId,
                FunctionName = "UpdateUser",
            });
        }

        public async System.Threading.Tasks.Task DeleteUser(int userId)
        {
            await _userRepository.DeleteUser(userId);
            await _serviceLogRepository.Add(new Models.DataModels.ServiceLog
            {
                UserId = userId,
                FunctionName = "DeleteUser",
            });
        }
    }
}
