using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using WorkTracker.Controllers;
using WorkTracker.Models.Requests;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Test
{
    public class AuthControllerTest
    {
        private readonly Mock<IAuthService> _authInterface;
        private readonly AuthController _authController;

        public AuthControllerTest()
        {
            _authInterface = new Mock<IAuthService>();
            _authController = new AuthController(_authInterface.Object);
        }

        [Test]
        public async Task DemoLogin_Successful()
        {
            string demoToken = "Test";
            _authInterface.Setup(x => x.DemoLogin()).ReturnsAsync(demoToken);
            _authController.ControllerContext = new ControllerContext();
            _authController.ControllerContext.HttpContext = new DefaultHttpContext();

            var token = await _authController.DemoLogin();
            Assert.IsInstanceOf<ActionResult>(token);
            Assert.IsNotNull(token);
        }

        [Test]
        public async Task DemoLogin_Unsuccessful()
        {
            string demoToken = null;
            _authInterface.Setup(x => x.DemoLogin()).ReturnsAsync(demoToken);

            var result = await _authController.DemoLogin();
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Demo login failed", ((BadRequestObjectResult)result).Value);
        }

        [Test]
        public async Task Login_Unsuccessful()
        {
            string response = null;
            var request = new UserLoginRequest()
            {
                Email = "qwerty",
                Password = "qwerty"
            };
            _authInterface.Setup(x => x.Login(request)).ReturnsAsync(response);

            var result = await _authController.Login(request);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("User validation failed", ((BadRequestObjectResult)result).Value);
        }

        [Test]
        public async Task Login_Successful()
        {
            string response = "token";
            var request = new UserLoginRequest()
            {
                Email = "test@email.com",
                Password = "password"
            };
            _authInterface.Setup(x => x.Login(request)).ReturnsAsync(response);
            _authController.ControllerContext = new ControllerContext();
            _authController.ControllerContext.HttpContext = new DefaultHttpContext();

            var result = await _authController.Login(request);
            Assert.IsInstanceOf<ActionResult>(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Register_Successful()
        {
            var request = new UserRegisterRequest();
            var result = await _authController.Register(request);
            Assert.IsInstanceOf<ActionResult>(result);
        }

    }
}