using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using WorkTracker.Controllers;
using WorkTracker.Models.Requests;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Test.Controllers
{
    public class ProjectControllerTest
    {
        private readonly Mock<IAuthService> _authInterface;
        private readonly AuthController _authController;

        public ProjectControllerTest()
        {
            _authInterface = new Mock<IAuthService>();
            _authController = new AuthController(_authInterface.Object);
        }

    }
}
