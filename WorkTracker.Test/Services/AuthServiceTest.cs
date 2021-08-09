using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using WorkTracker.Models;
using WorkTracker.Models.Requests;
using WorkTracker.Models.ServiceModels;
using WorkTracker.Repositories.Interfaces;
using WorkTracker.Services;

namespace WorkTracker.Test.Services
{
    public class AuthServiceTest
    {
        private readonly Mock<IRoleRepository> _roleRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ITeamRepository> _teamRepository;
        private readonly Mock<IStateRepository> _stateRepository;
        private readonly Mock<IServiceLogRepository> _serviceLogRepository;
        private readonly AppSettings _appSettings;
        private readonly AuthService _authService;
        public AuthServiceTest()
        {
            _roleRepository = new Mock<IRoleRepository>();
            _userRepository = new Mock<IUserRepository>();
            _teamRepository = new Mock<ITeamRepository>();
            _stateRepository = new Mock<IStateRepository>();
            _serviceLogRepository = new Mock<IServiceLogRepository>();
            _appSettings = Helper.GetAppSettings();
            var optionParameter = Options.Create(_appSettings);
            _authService = new AuthService(
                _roleRepository.Object,
                _userRepository.Object,
                _teamRepository.Object,
                _stateRepository.Object,
                _serviceLogRepository.Object,
                optionParameter
            );
        }

        [Test]
        public async System.Threading.Tasks.Task CreateToken_Successful()
        {
            var repoResponse = new Role
            {
                Name = "test_role",
                Permissions = "create_story,view_story,edit_story,create_user",
                RoleId = 1
            };
            _roleRepository.Setup(x => x.GetUserRole(0)).ReturnsAsync(repoResponse);

            var permissions = new string[]
            {
                "create_story",
                "view_story",
                "edit_story",
                "create_user"
            };
            var expectedResult = WorkTracker.Services.Helper.GenerateToken(0, permissions, _appSettings.JwtSecret);

            var result = await _authService.CreateToken(0);
            Assert.IsInstanceOf<string>(result);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async System.Threading.Tasks.Task CreateToken_NoPermissions()
        {
            var repoResponse = new Role
            {
                Name = "test_role",
                Permissions = "",
                RoleId = 1
            };
            _roleRepository.Setup(x => x.GetUserRole(0)).ReturnsAsync(repoResponse);

            var permissions = "".Split(',');
            var expectedResult = WorkTracker.Services.Helper.GenerateToken(0, permissions, _appSettings.JwtSecret);

            var result = await _authService.CreateToken(0);
            Assert.IsInstanceOf<string>(result);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async System.Threading.Tasks.Task CreateToken_MissingRole()
        {
            Role repoResponse = null;
            _roleRepository.Setup(x => x.GetUserRole(0)).ReturnsAsync(repoResponse);

            var expectedResult = new Exception("User role is null");

            try
            {
                var result = await _authService.CreateToken(0);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOf<Exception>(e);
                Assert.AreEqual(expectedResult.Message, e.Message);
            }
        }

        [Test]
        public async System.Threading.Tasks.Task RefreshToken_Successful()
        {
            var repoResponse = new Role
            {
                Name = "test_role",
                Permissions = "create_story,view_story,edit_story,create_user",
                RoleId = 1
            };
            _roleRepository.Setup(x => x.GetUserRole(0)).ReturnsAsync(repoResponse);

            var permissions = new string[]
            {
                "create_story",
                "view_story",
                "edit_story",
                "create_user"
            };
            var expectedResult = WorkTracker.Services.Helper.GenerateToken(0, permissions, _appSettings.JwtSecret);

            var result = await _authService.RefreshToken(expectedResult);
            Assert.IsInstanceOf<string>(result);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async System.Threading.Tasks.Task Register_Successful()
        {
            var repoUserResponse = new Models.DataModels.User
            {
                Email = "",
                Name = "",
                Password = "",
                RoleId = 1,
                UserId = 1,
            };
            var newUser = new Models.DataModels.User
            {
                Email = "",
                Name = "",
                Password = "",
                RoleId = 1,
                UserId = 1,
            };
            _userRepository.Setup(x => x.Add(newUser)).ReturnsAsync(repoUserResponse);

            var repoTeamResponse = new Models.DataModels.Team
            {
                OrganisationId = 1,
                TeamId = 1,
                Name = Guid.NewGuid().ToString()
            };
            var newTeam = new Models.DataModels.Team
            {
                OrganisationId = 1,
                TeamId = 1,
                Name = Guid.NewGuid().ToString()
            };
            _teamRepository.Setup(x => x.Add(newTeam)).ReturnsAsync(repoTeamResponse);
            _teamRepository.Setup(x => x.AssignUser(1, 1));
            _stateRepository.Setup(x => x.CreateDefaultStates(1));
            var request = new UserRegisterRequest
            {
                Email = "test@email.com",
                Password = "password",
                Name = "test",
                TeamId = 1,
                RoleId = 1
            };
            try
            {
                await _authService.Register(request);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsFalse(true);
            }
        }
    }
}
