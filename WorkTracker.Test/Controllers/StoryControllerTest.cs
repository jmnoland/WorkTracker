using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Controllers;
using WorkTracker.Models.Requests;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Test.Controllers
{
    class StoryControllerTest
    {
        private readonly Mock<IStoryService> _storyInterface;
        private readonly StoryController _storyController;
        private readonly int _demoUserId = 22;
        private readonly string _token;

        public StoryControllerTest()
        {
            _storyInterface = new Mock<IStoryService>();
            _storyController = new StoryController(_storyInterface.Object);
            _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyMiIsIlVzZXJSb2xlIjpbImNyZWF0ZV91c2VyIiwiZGVsZXRlX3VzZXIiLCJjcmVhdGVfc3RvcnkiLCJ2aWV3X3N0b3J5Il0sIm5iZiI6MTYyNTA4Njk2MSwiZXhwIjoxNjI1MDg4NzYxLCJpYXQiOjE2MjUwODY5NjF9.rMSgeQN9MQdkFg0NBpuUcNnOHBTtAGn46CjHKQT5xR8";
        }

        [SetUp]
        public void Setup()
        {
            _storyController.ControllerContext = new ControllerContext();
            var defaultContext = new DefaultHttpContext();
            defaultContext.Request.Headers["X-User-Token"] = _token;
            _storyController.ControllerContext.HttpContext = defaultContext;
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

