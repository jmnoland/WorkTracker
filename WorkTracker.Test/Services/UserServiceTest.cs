using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WorkTracker.Repositories.Interfaces;
using WorkTracker.Services;

namespace WorkTracker.Test.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IRoleRepository> _roleRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ITeamRepository> _teamRepository;
        private readonly Mock<IStateRepository> _stateRepository;
        private readonly Mock<IServiceLogRepository> _serviceLogRepository;
        private readonly UserService _userService;
        
        public UserServiceTest()
        {
            _roleRepository = new Mock<IRoleRepository>();
            _userRepository = new Mock<IUserRepository>();
            _teamRepository = new Mock<ITeamRepository>();
            _stateRepository = new Mock<IStateRepository>();
            _serviceLogRepository = new Mock<IServiceLogRepository>();
            _userService = new UserService(
                _userRepository.Object,
                _roleRepository.Object,
                _teamRepository.Object,
                _stateRepository.Object,
                _serviceLogRepository.Object
            );
        }

        [Test]
        public async Task GetAllUsers_Successful()
        {
            var response = new List<Models.ServiceModels.User>
            {
                new Models.ServiceModels.User
                {
                    Email = "test@email.com",
                    Name = "test",
                    Password = "password",
                    RoleId = 1,
                    UserId = 2
                }
            };
            _userRepository.Setup(x => x.GetAllUsers(0)).ReturnsAsync(response);

            var result = await _userService.GetAllUsers(0);
            Assert.IsInstanceOf<List<Models.DTOs.User>>(result);
            Assert.Multiple(() =>
            {
                var expected = response.First();
                result.ForEach(user =>
                {
                    Assert.AreEqual(expected.Email, user.Email);
                    Assert.AreEqual(expected.Name, user.Name);
                    Assert.AreEqual(expected.RoleId, user.RoleId);
                    Assert.AreEqual(expected.UserId, user.UserId);
                });
            });
        }
        
        [Test]
        public async Task GetAllUsers_Empty()
        {
            var response = new List<Models.ServiceModels.User>();
            _userRepository.Setup(x => x.GetAllUsers(0)).ReturnsAsync(response);

            var result = await _userService.GetAllUsers(0);
            Assert.IsInstanceOf<List<Models.DTOs.User>>(result);
            Assert.IsTrue(!result.Any());
        }
        
        [Test]
        public async Task GetUsersByTeamId_Successful()
        {
            var teamResponse = new List<Models.ServiceModels.Team>
            {
                new Models.ServiceModels.Team
                {
                    Name = "test Team",
                    OrganisationId = 1,
                    TeamId = 2
                }
            };
            var userResponse = new Models.ServiceModels.User
            {
                Email = "test@memail.com",
                Name = "test",
                Password = "password",
                RoleId = 1,
                UserId = 2
            };
            var userListResponse = new List<Models.ServiceModels.User>
            {
                userResponse
            };
            _teamRepository.Setup(x => x.GetByUserId(0)).ReturnsAsync(teamResponse);
            _userRepository.Setup(x => x.GetUsersByTeamId(new List<int>{ 2 }))
                .ReturnsAsync(userListResponse);
            
            var result = await _userService.GetUsersByTeamId(0);
            Assert.IsInstanceOf<List<Models.DTOs.User>>(result);
            Assert.IsTrue(result.Any());
        }
        
        [Test]
        public async Task GetUsersByTeamId_Empty()
        {
            var teamResponse = new List<Models.ServiceModels.Team>();
            var userListResponse = new List<Models.ServiceModels.User>();
            _teamRepository.Setup(x => x.GetByUserId(0)).ReturnsAsync(teamResponse);
            _userRepository.Setup(x => x.GetUsersByTeamId(new List<int>()))
                .ReturnsAsync(userListResponse);

            var result = await _userService.GetUsersByTeamId(0);
            Assert.IsInstanceOf<List<Models.DTOs.User>>(result);
            Assert.IsTrue(!result.Any());
        }
        
        [Test]
        public async Task GetUserDetail_Successful()
        {
            var teamResponse = new List<Models.ServiceModels.Team>
            {
                new Models.ServiceModels.Team
                {
                    Name = "team",
                    OrganisationId = 1,
                    TeamId = 2
                }
            };
            var userListResponse = new List<Models.ServiceModels.User>
            {
                new Models.ServiceModels.User
                {
                    Email = "test@email.com",
                    Name = "test",
                    Password = "password",
                    RoleId = 3,
                    UserId = 4
                }
            };
            var stateList = new List<Models.ServiceModels.State>
            {
                new Models.ServiceModels.State
                {
                    Name = "state",
                    Type = "type",
                    StateId = 5,
                    TeamId = 2
                }
            };
            _teamRepository.Setup(x => x.GetByUserId(0)).ReturnsAsync(teamResponse);
            _userRepository.Setup(x => x.GetAllUsers(2))
                .ReturnsAsync(userListResponse);
            _stateRepository.Setup(x => x.GetByTeamId(2)).ReturnsAsync(stateList);

            var result = await _userService.GetUserDetail(0);
            Assert.IsInstanceOf<Models.DTOs.UserDetail>(result);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.Users.Any());
                Assert.IsTrue(result.States.Any());
                Assert.IsTrue(result.Teams.Any());
            });
        }
        
        [Test]
        public async Task GetUserDetail_Empty()
        {
            var teamResponse = new List<Models.ServiceModels.Team>();
            var userListResponse = new List<Models.ServiceModels.User>();
            _teamRepository.Setup(x => x.GetByUserId(0)).ReturnsAsync(teamResponse);

            var expected = new Models.DTOs.UserDetail()
            {
                States = new List<Models.DTOs.State>(),
                Teams = new List<Models.DTOs.Team>(),
                Users = new List<Models.DTOs.User>()
            };
            var result = await _userService.GetUserDetail(0);
            Assert.IsInstanceOf<Models.DTOs.UserDetail>(result);
            Assert.IsFalse(result.Users.Any());
            Assert.IsFalse(result.States.Any());
            Assert.IsFalse(result.Teams.Any());
        }
        
        
    }
}