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
                Assert.IsTrue(false);
            }
        }
        [Test]
        public async Task CreateStory_WithTasks_Successful()
        {
            _storyRepository.Setup(x => x.CreateStory(0, new Models.ServiceModels.Story()))
                .ReturnsAsync(0);

            var tasks = new List<Models.DTOs.Task>
            {
                new()
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
                Assert.IsTrue(false);
            }
        }

        [Test]
        public async Task UpdateStory_StoryNotFound()
        {
            _storyRepository.Setup(x => x.UpdateStory(new Models.ServiceModels.Story(), 0))
                .Throws(new Exception("Update failed story not found"));
            var request = new UpdateStoryRequest
            {
                Description = "Story description",
                ListOrder = 0,
                ProjectId = 0,
                SprintId = 0,
                StateId = 0,
                Title = "Test Story"
            };
            try
            {
                await _storyService.UpdateStory(request, 0);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOf<Exception>(e);
                Assert.AreEqual("Update failed story not found", e.Message);
            }
        }
        
        [Test]
        public async Task UpdateStory_Successful()
        {
            _storyRepository.Setup(x => x.UpdateStory(new Models.ServiceModels.Story(), 0));
            var request = new UpdateStoryRequest
            {
                Description = "Story description",
                ListOrder = 0,
                ProjectId = 0,
                SprintId = 0,
                StateId = 0,
                Title = "Test Story"
            };
            try
            {
                await _storyService.UpdateStory(request, 0);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }

        [Test]
        public async Task UpdateStory_WithTasks_Successful()
        {
            _storyRepository.Setup(x => x.UpdateStory(new Models.ServiceModels.Story(), 0));
            _storyRepository.Setup(x => x.UpdateTasks(new List<Models.ServiceModels.Task>()));
            var tasks = new List<Models.DTOs.Task>
            {
                new()
                {
                    StoryId = 0,
                    TaskId = 0,
                    Complete = false,
                    Description = "task description"
                }
            };
            var request = new UpdateStoryRequest
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
                await _storyService.UpdateStory(request, 0);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        
        [Test]
        public async Task DeleteStory_StoryNotFound()
        {
            _storyRepository.Setup(x => x.DeleteStory(0, 0))
                .Throws(new Exception("Delete failed story not found"));
            try
            {
                await _storyService.DeleteStory(0, 0);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOf<Exception>(e);
                Assert.AreEqual("Delete failed story not found", e.Message);
            }
        }
        
        [Test]
        public async Task DeleteStory_Successful()
        {
            _storyRepository.Setup(x => x.DeleteStory(0, 0));
            try
            {
                await _storyService.DeleteStory(0, 0);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }
        
        [Test]
        public async Task OrderUpdate_StoryNotFound()
        {
            _storyRepository.Setup(x => x.DeleteStory(0, 0))
                .Throws(new Exception("Order update failed stories not found"));
            var request = new OrderUpdateRequest
            {
                StateId = 0,
                Stories = new Dictionary<string, int>()
            };
            try
            {
                await _storyService.OrderUpdate(0, request);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOf<Exception>(e);
                Assert.AreEqual("Order update failed stories not found", e.Message);
            }
        }
        
        [Test]
        public async Task OrderUpdate_Successful()
        {
            _storyRepository.Setup(x => x.OrderUpdate(0, 0, new Dictionary<string, int>()));
            var request = new OrderUpdateRequest
            {
                StateId = 0,
                Stories = new Dictionary<string, int>()
            };
            try
            {
                await _storyService.OrderUpdate(0, request);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }
        
        [Test]
        public async Task GetStoryTasks_Successful()
        {
            var repoResponse = new List<Models.ServiceModels.Task>
            {
                new()
                {
                    StoryId = 0,
                    Complete = false,
                    TaskId = 0,
                    Description = "test"
                }
            };
            _storyRepository.Setup(x => x.GetStoryTasks(0, 0))
                .ReturnsAsync(repoResponse);
            var expectedResponse = new Models.DTOs.Task
            {
                StoryId = 0,
                Complete = false,
                TaskId = 0,
                Description = "test"
            };
            var response = await _storyService.GetStoryTasks(0, 0);
            Assert.IsInstanceOf<List<Models.DTOs.Task>>(response);
            foreach (var task in response)
            {
                Assert.AreEqual(expectedResponse.Complete, task.Complete);
                Assert.AreEqual(expectedResponse.Description, task.Description);
                Assert.AreEqual(expectedResponse.StoryId, task.StoryId);
                Assert.AreEqual(expectedResponse.TaskId, task.TaskId);
            }
        }
        
        [Test]
        public async Task GetStoryTasks_Failed()
        {
            const int userId = 0;
            _storyRepository.Setup(x => x.GetStoryTasks(0, userId))
                .Throws(new Exception($"Task not found for user {userId}"));
            try
            {
                await _storyService.GetStoryTasks(0, userId);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOf<Exception>(e);
                Assert.AreEqual("Task not found for user 0", e.Message);
            }
        }
        
        [Test]
        public async Task DeleteTask_Successful()
        {
            const int userId = 0;
            _storyRepository.Setup(x => x.DeleteTask(0, userId));

            await _storyService.DeleteTask(0, userId);
            Assert.IsTrue(true);
        }
        
        [Test]
        public async Task DeleteTask_Failed()
        {
            const int userId = 0;
            _storyRepository.Setup(x => x.DeleteTask(0, userId))
                .Throws(new Exception("Delete failed story not found"));
            try
            {
                await _storyService.DeleteTask(0, userId);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOf<Exception>(e);
                Assert.AreEqual("Delete failed story not found", e.Message);
            }
        }
        
        [Test]
        public async Task ChangeState_Successful()
        {
            const int userId = 0;
            _storyRepository.Setup(x => x.ChangeState(userId, 0, 0));
            _storyRepository.Setup(x => x.OrderUpdate(0, userId, new Dictionary<string, int>()));

            var request = new OrderUpdateRequest
            {
                Stories = new Dictionary<string, int>(),
                StateId = 0
            };
            await _storyService.ChangeState(userId, 0, request);
            Assert.IsTrue(true);
        }
        
        [Test]
        public async Task ChangeState_Failed()
        {
            const int userId = 0;
            _storyRepository.Setup(x => x.ChangeState(userId, 0, 0))
                .Throws(new Exception("Change state failed story not found"));
            _storyRepository.Setup(x => x.OrderUpdate(0, userId, new Dictionary<string, int>()));

            var request = new OrderUpdateRequest
            {
                Stories = new Dictionary<string, int>(),
                StateId = 0
            };
            try
            {
                await _storyService.ChangeState(userId, 0, request);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOf<Exception>(e);
                Assert.AreEqual("Change state failed story not found", e.Message);
            }
        }
    }
}
