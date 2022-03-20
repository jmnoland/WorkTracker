using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Primitives;
using WorkTracker.Controllers;
using WorkTracker.Models;
using WorkTracker.Models.DTOs;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Test.Controllers
{
    public class ProjectControllerTest
    {
        private readonly Mock<IProjectService> _projectInterface;
        private readonly ProjectController _projectController;
        private readonly AppSettings _appSettings;
        private readonly int _demoUserId = 22;
        private readonly string _token;
        
        public ProjectControllerTest()
        {
            _projectInterface = new Mock<IProjectService>();
            _projectController = new ProjectController(_projectInterface.Object);
            
            _appSettings = Helper.GetAppSettings();
            var permissions = new string[]
            {
                "create_story",
                "create_user",
                "view_story",
                "edit_story",
                "view_project"
            };
            _token = WorkTracker.Services.Helper.GenerateToken(_demoUserId, permissions, _appSettings.JwtSecret);
        }

        [SetUp]
        public void Setup()
        {
            _projectController.ControllerContext = new ControllerContext();
            _projectController.ControllerContext.HttpContext = new DefaultHttpContext();
            var header = new KeyValuePair<string, StringValues>
            (
                "Authorization",
                $"Bearer {_token}"
            );
            _projectController.ControllerContext.HttpContext.Request.Headers.Add(header);
        }

        [Test]
        public async System.Threading.Tasks.Task GetProjectByTeamId_Missing_Token()
        {
            var response = new Dictionary<int, List<Project>>();
            _projectInterface.Setup(x => x.GetByUserId(_demoUserId))
                .ReturnsAsync(response);
            
            _projectController.ControllerContext = new ControllerContext();
            _projectController.ControllerContext.HttpContext = new DefaultHttpContext();

            var result = await _projectController.GetProjectByTeamId();
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("User id missing", ((BadRequestObjectResult)result).Value);
        }

        [Test]
        public async System.Threading.Tasks.Task GetProjectByTeamId_Successful()
        {
            var response = new Dictionary<int, List<Project>>();
            _projectInterface.Setup(x => x.GetByUserId(_demoUserId))
                .ReturnsAsync(response);

            var result = await _projectController.GetProjectByTeamId();
            Assert.IsInstanceOf<ActionResult>(result);
            var value = (OkObjectResult)result;
            Assert.IsInstanceOf<Dictionary<int, List<Project>>>(value.Value);
        }
    }
}
