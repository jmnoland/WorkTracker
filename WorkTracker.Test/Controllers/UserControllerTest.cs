using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Controllers;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Test.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _userService;
        private readonly UserController _userController;

        public UserControllerTest()
        {
            _userService = new Mock<IUserService>();
            _userController = new UserController(_userService.Object);
        }

        [Test]
        public async Task GetUsers_Successful()
        {
            var result = new List<Models.DTOs.User>();
            _userService.Setup(x => x.GetUsersByTeamId(0)).ReturnsAsync(result);
            _userController.ControllerContext = new ControllerContext();
            _userController.ControllerContext.HttpContext = new DefaultHttpContext();

            var response = await _userController.GetUsers(0);
            Assert.IsInstanceOf<ActionResult>(response);
            var responseValue = ((OkObjectResult)response).Value;
            Assert.IsInstanceOf<List<Models.DTOs.User>>(responseValue);
        }
    }
}