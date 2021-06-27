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
    class StoryControllerTest
    {
        private readonly Mock<IStoryService> _storyInterface;
        private readonly StoryController _storyController;

        public StoryControllerTest()
        {
            _storyInterface = new Mock<IStoryService>();
            _storyController = new StoryController(_storyInterface.Object);
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
            Assert.AreEqual("User token missing", ((BadRequestObjectResult)result).Value);
        }
    }
}
