using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTracker.Models.Requests;
using WorkTracker.Repositories.Interfaces;
using WorkTracker.Services;

namespace WorkTracker.Test.Services
{
    public class StoryServiceTest
    {
        private readonly Mock<IStoryRepository> _storyRepository;
        private readonly Mock<IServiceLogRepository> _serviceLogRepository;
        private readonly StoryService _storyService;
        public StoryServiceTest()
        {
            _storyRepository = new Mock<IStoryRepository>();
            _serviceLogRepository = new Mock<IServiceLogRepository>();
            _storyService = new StoryService(_storyRepository.Object, _serviceLogRepository.Object);
        }

        [Test]
        public async Task GetStoriesByStateId_Successful()
        {
            var repoResponse = new List<Models.ServiceModels.Story>();
            _storyRepository.Setup(x => x.GetStoriesByStateId(0, 0, false)).ReturnsAsync(repoResponse);

            var result = await _storyService.GetStoriesByStateId(0, 0, false);

            Assert.IsInstanceOf<List<Models.DTOs.Story>>(result);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public async Task GetStoriesByStateId_Archived_Successful()
        {
            var repoResponse = new List<Models.ServiceModels.Story>();
            _storyRepository.Setup(x => x.GetStoriesByStateId(0, 0, true)).ReturnsAsync(repoResponse);

            var result = await _storyService.GetStoriesByStateId(0, 0, true);

            Assert.IsInstanceOf<List<Models.DTOs.Story>>(result);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public async Task CreateStory_Successful()
        {
            _storyRepository.Setup(x => x.CreateStory(0, new Models.ServiceModels.Story()))
                .ReturnsAsync(0);

            var request = new CreateStoryRequest();
            try
            {
                await _storyService.CreateStory(0, request);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsFalse(true);
            }
        }
        [Test]
        public async Task CreateStory_WithTasks_Successful()
        {
            _storyRepository.Setup(x => x.CreateStory(0, new Models.ServiceModels.Story()))
                .ReturnsAsync(0);

            var tasks = new List<Models.DTOs.Task>
            {
                new Models.DTOs.Task
                {
                    StoryId = 0,
                    TaskId = 0,
                    Complete = false,
                    Description = "task description"
                }
            };
            var request = new CreateStoryRequest
            {
                Description = "Story description",
                ListOrder = 0,
                ProjectId = 0,
                SprintId = 0,
                StateId = 0,
                Title = "Test Story",
                Tasks = tasks
            };
            try
            {
                await _storyService.CreateStory(0, request);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsFalse(true);
            }
        }
    }
}
