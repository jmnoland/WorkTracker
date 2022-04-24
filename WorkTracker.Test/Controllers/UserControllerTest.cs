using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Controllers;
using WorkTracker.Models;
using WorkTracker.Models.Requests;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Test.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _userService;
        private readonly UserController _userController;
        private readonly AppSettings _appSettings;
        private readonly string _token;
        public UserControllerTest()
        {
            _appSettings = Helper.GetAppSettings();
            var permissions = new string[]
            {
                "create_story",
                "create_user",
                "view_story",
                "edit_story"
            };
            _token = WorkTracker.Services.Helper.GenerateToken(0, null, permissions, _appSettings.JwtSecret);

            _userService = new Mock<IUserService>();
            _userController = new UserController(_userService.Object);
        }

        [SetUp]
        public void Setup()
        {
            _userController.ControllerContext = new ControllerContext();
            _userController.ControllerContext.HttpContext = new DefaultHttpContext();
            var header = new KeyValuePair<string, StringValues>
            (
                "Authorization",
                $"Bearer {_token}"
            );
            _userController.ControllerContext.HttpContext.Request.Headers.Add(header);
        }

        [Test]
        public async Task GetUsers_Successful()
        {
            var result = new List<Models.DTOs.User>
            {
                new Models.DTOs.User
                {
                    Email = "test@email.com",
                    Name = "test",
                    RoleId = 1,
                    UserId = 2
                }
            };
            _userService.Setup(x => x.GetUsersByTeamId(0)).ReturnsAsync(result);

            var response = await _userController.GetUsers();
            Assert.IsInstanceOf<OkObjectResult>(response);
            var responseValue = ((OkObjectResult)response).Value;
            Assert.IsInstanceOf<List<Models.DTOs.User>>(responseValue);
        }
        [Test]
        public async Task GetUsers_NoResponse()
        {
            var result = new List<Models.DTOs.User>();
            _userService.Setup(x => x.GetUsersByTeamId(0)).ReturnsAsync(result);

            var response = await _userController.GetUsers();
            Assert.IsInstanceOf<ActionResult>(response);
            Assert.IsInstanceOf<NoContentResult>(response);
        }
        [Test]
        public async Task GetUserDetails_Successful()
        {
            var result = new Models.DTOs.UserDetail()
            {
                States = new List<Models.DTOs.State>(),
                Teams = new List<Models.DTOs.Team>(),
                Users = new List<Models.DTOs.User>
                {
                    new Models.DTOs.User
                    {
                        Email = "test@email.com",
                        Name = "test",
                        RoleId = 1,
                        UserId = 2
                    }
                }
            };
            _userService.Setup(x => x.GetUserDetail(0)).ReturnsAsync(result);

            var response = await _userController.GetUserDetails();
            Assert.IsInstanceOf<ActionResult>(response);
            Assert.IsInstanceOf<OkObjectResult>(response);
            var responseValue = ((OkObjectResult)response).Value;
            Assert.IsInstanceOf<Models.DTOs.UserDetail>(responseValue);
        }
        [Test]
        public async Task GetUserDetails_NoResponse()
        {
            var result = new Models.DTOs.UserDetail()
            {
                States = new List<Models.DTOs.State>(),
                Teams = new List<Models.DTOs.Team>(),
                Users = new List<Models.DTOs.User>()
            };
            _userService.Setup(x => x.GetUserDetail(0)).ReturnsAsync(result);

            var response = await _userController.GetUserDetails();
            Assert.IsInstanceOf<ActionResult>(response);
            Assert.IsInstanceOf<NoContentResult>(response);
        }
        [Test]
        public async Task CreateUser_Successful()
        {
            var request = new CreateUserRequest()
            {
                Email = "test@email.com",
                Name = "test",
                Password = "pass",
                RoleId = 1
            };
            _userService.Setup(x => x.CreateUser(request));

            var response = await _userController.CreateUser(request);
            Assert.IsInstanceOf<ActionResult>(response);
            Assert.IsInstanceOf<OkObjectResult>(response);
            var responseValue = ((OkObjectResult)response).Value;
            Assert.IsInstanceOf<string>(responseValue);
            Assert.AreEqual("User created successfully", responseValue);
        }
        [Test]
        public async Task CreateUser_InvalidRequest()
        {
            var request = new CreateUserRequest();
            _userService.Setup(x => x.CreateUser(request));

            var response = await _userController.CreateUser(request);
            Assert.IsInstanceOf<ActionResult>(response);
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var responseValue = ((BadRequestObjectResult)response).Value;
            Assert.IsInstanceOf<string>(responseValue);
            Assert.AreEqual("Invalid request", responseValue);
        }
        [Test]
        public async Task CreateUser_RoleDoesNotExist()
        {
            var request = new CreateUserRequest()
            {
                Email = "test@email.com",
                Name = "test",
                Password = "pass",
                RoleId = 1
            };
            _userService.Setup(x => x.CreateUser(request))
                .Throws(new ArgumentNullException());

            var response = await _userController.CreateUser(request);
            Assert.IsInstanceOf<ActionResult>(response);
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var responseValue = ((BadRequestObjectResult)response).Value;
            Assert.IsInstanceOf<string>(responseValue);
            Assert.AreEqual("Role does not exist", responseValue);
        }
        [Test]
        public async Task RegisterUser_Successful()
        {
            var request = new CreateUserRequest()
            {
                Email = "test@email.com",
                Name = "test",
                Password = "pass",
                RoleId = 0
            };
            _userService.Setup(x => x.CreateUser(request));

            var response = await _userController.RegisterUser(request);
            Assert.IsInstanceOf<ActionResult>(response);
            Assert.IsInstanceOf<OkObjectResult>(response);
            var responseValue = ((OkObjectResult)response).Value;
            Assert.IsInstanceOf<string>(responseValue);
            Assert.AreEqual("User registered successfully", responseValue);
        }
        [Test]
        public async Task RegisterUser_InvalidRequest()
        {
            var request = new CreateUserRequest();
            _userService.Setup(x => x.CreateUser(request));

            var response = await _userController.RegisterUser(request);
            Assert.IsInstanceOf<ActionResult>(response);
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var responseValue = ((BadRequestObjectResult)response).Value;
            Assert.IsInstanceOf<string>(responseValue);
            Assert.AreEqual("Invalid request", responseValue);
        }
        [Test]
        public async Task UpdateUser_Successful()
        {
            var request = new UpdateUserRequest()
            {
                UserId = 0,
                Email = "test@email.com",
                Name = "test",
                Password = "pass",
                RoleId = 0
            };
            _userService.Setup(x => x.UpdateUser(request));

            var response = await _userController.UpdateUser(request);
            Assert.IsInstanceOf<ActionResult>(response);
            Assert.IsInstanceOf<OkObjectResult>(response);
            var responseValue = ((OkObjectResult)response).Value;
            Assert.IsInstanceOf<string>(responseValue);
            Assert.AreEqual("User updated successfully", responseValue);
        }
        [Test]
        public async Task UpdateUser_InvalidRequest()
        {
            var request = new UpdateUserRequest();
            _userService.Setup(x => x.UpdateUser(request));

            var response = await _userController.UpdateUser(request);
            Assert.IsInstanceOf<ActionResult>(response);
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var responseValue = ((BadRequestObjectResult)response).Value;
            Assert.IsInstanceOf<string>(responseValue);
            Assert.AreEqual("Invalid request", responseValue);
        }
        [Test]
        public async Task UpdateUser_ServiceUpdateFail()
        {
            var request = new UpdateUserRequest()
            {
                UserId = 0,
                Email = "test@email.com",
                Name = "test",
                Password = "pass",
                RoleId = 0
            };
            _userService.Setup(x => x.UpdateUser(request))
                .Throws(new Exception());
            var response = await _userController.UpdateUser(request);
            Assert.IsInstanceOf<ActionResult>(response);
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var responseValue = ((BadRequestObjectResult)response).Value;
            Assert.IsInstanceOf<string>(responseValue);
            Assert.AreEqual("User update failed", responseValue);
        }
        [Test]
        public async Task RemoveUser_Successful()
        {
            _userService.Setup(x => x.DeleteUser(0));

            var response = await _userController.RemoveUser(0);
            Assert.IsInstanceOf<ActionResult>(response);
            Assert.IsInstanceOf<OkObjectResult>(response);
            var responseValue = ((OkObjectResult)response).Value;
            Assert.IsInstanceOf<string>(responseValue);
            Assert.AreEqual("User removed successfully", responseValue);
        }
        [Test]
        public async Task RemoveUser_ServiceDeleteFail()
        {
            _userService.Setup(x => x.DeleteUser(0)).Throws(new Exception());

            var response = await _userController.RemoveUser(0);
            Assert.IsInstanceOf<ActionResult>(response);
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var responseValue = ((BadRequestObjectResult)response).Value;
            Assert.IsInstanceOf<string>(responseValue);
            Assert.AreEqual("Unable to remove user", responseValue);
        }
    }
}