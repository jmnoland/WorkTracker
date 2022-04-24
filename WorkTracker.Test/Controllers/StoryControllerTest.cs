using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Controllers;
using WorkTracker.Models;
using WorkTracker.Models.Requests;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Test.Controllers
{
    class StoryControllerTest
    {
        private readonly Mock<IStoryService> _storyInterface;
        private readonly StoryController _storyController;
        private readonly AppSettings _appSettings;
        private readonly int _demoUserId = 22;
        private readonly string _token;

        public StoryControllerTest()
        {
            _storyInterface = new Mock<IStoryService>();
            _storyController = new StoryController(_storyInterface.Object);

            _appSettings = Helper.GetAppSettings();
            var permissions = new string[]
            {
                "create_story",
                "create_user",
                "view_story",
                "edit_story"
            };
            _token = WorkTracker.Services.Helper.GenerateToken(_demoUserId, null, permissions, _appSettings.JwtSecret);
        }

        [SetUp]
        public void Setup()
        {
            _storyController.ControllerContext = new ControllerContext();
            _storyController.ControllerContext.HttpContext = new DefaultHttpContext();
            var header = new KeyValuePair<string, StringValues>
            (
                "Authorization",
                $"Bearer {_token}"
            );
            _storyController.ControllerContext.HttpContext.Request.Headers.Add(header);
        }

        [Test]
        public async Task GetStoriesByStateId_Missing_Token()
        {
            var response = new List<Models.DTOs.Story>();
            _storyInterface.Setup(x => x.GetStoriesByStateId(0, 0, false)).ReturnsAsync(response);
            _storyController.ControllerContext = new ControllerContext();
            _storyController.ControllerContext.HttpContext = new DefaultHttpContext();

            var result = await _storyController.GetStoriesByStateId(0);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("User id missing", ((BadRequestObjectResult)result).Value);
        }


        [Test]
        public async Task GetStoriesByStateId_Successful()
        {
            var response = new List<Models.DTOs.Story>();
            _storyInterface.Setup(x => x.GetStoriesByStateId(_demoUserId, 0, false)).ReturnsAsync(response);

            var result = await _storyController.GetStoriesByStateId(0);
            Assert.IsInstanceOf<ActionResult>(result);
            var value = (OkObjectResult)result;
            Assert.IsInstanceOf<List<Models.DTOs.Story>>(value.Value);
        }

        [Test]
        public async Task CreateStory_Valid_Request()
        {
            var request = new CreateStoryRequest()
            {
                Title = "Title",
                Description = "Description",
                ListOrder = 0,
                ProjectId = 0,
                SprintId = 0,
                StateId = 0
            };
            var result = await _storyController.CreateStory(request);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task CreateStory_Invalid_Request()
        {
            var request = new CreateStoryRequest()
            {
                Title = null,
                Description = "Description",
                ListOrder = 0,
                ProjectId = 0,
                SprintId = 0,
                StateId = 0
            };
            var result = await _storyController.CreateStory(request);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var value = (BadRequestObjectResult)result;
            Assert.AreEqual("Request validation failed", value.Value);
        }

        [Test]
        public async Task UpdateStory_Valid_Request()
        {
            var request = new UpdateStoryRequest()
            {
                Title = "Title",
                Description = "Description",
                ListOrder = 0,
                ProjectId = 0,
                SprintId = 0,
                StateId = 0,
            };
            var result = await _storyController.UpdateStory(request);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task UpdateStory_Invalid_Request()
        {
            var request = new UpdateStoryRequest()
            {
                Title = "",
                Description = "Description",
                ListOrder = 0,
                ProjectId = 0,
                SprintId = 0,
                StateId = 0,
            };
            var result = await _storyController.UpdateStory(request);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var value = (BadRequestObjectResult)result;
            Assert.AreEqual("Request validation failed", value.Value);
        }

        [Test]
        public async Task ChangeState_Valid_Request()
        {
            var request = new OrderUpdateRequest()
            {
                StateId = 0,
                Stories = new Dictionary<string, int>
                {
                    { "0", 0 }
                }
            };
            var result = await _storyController.ChangeState(0, request);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task ChangeState_Invalid_Request()
        {
            var request = new OrderUpdateRequest()
            {
                StateId = 0,
                Stories = new Dictionary<string, int>()
            };
            var result = await _storyController.ChangeState(0, request);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var value = (BadRequestObjectResult)result;
            Assert.AreEqual("Request validation failed", value.Value);
        }
    }
}

