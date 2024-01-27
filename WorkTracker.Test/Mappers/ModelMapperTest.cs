using NUnit.Framework;
using System;
using System.Collections.Generic;
using WorkTracker.Models;
using WorkTracker.Models.Requests;

namespace WorkTracker.Test.Mappers
{
    public class ModelMapperTest
    {
        [Test]
        public void User_ServiceModelMapping()
        {
            var testUser = new Models.ServiceModels.User
            {
                Email = "test@email.com",
                Name = "test name",
                Password = "password",
                RoleId = 1,
                UserId = 2
            };
            var response = Mapper.Map(testUser);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testUser.Email, response.Email);
                Assert.AreEqual(testUser.Name, response.Name);
                Assert.AreEqual(testUser.RoleId, response.RoleId);
                Assert.AreEqual(testUser.UserId, response.UserId);
            });
            Assert.IsInstanceOf<Models.DTOs.User>(response);
        }

        [Test]
        public void Task_ServiceModelMapping()
        {
            var testTask = new Models.ServiceModels.Task
            {
                Complete = false,
                Description = "description",
                StoryId = 1,
                TaskId = 2
            };
            var response = Mapper.Map(testTask);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testTask.Complete, response.Complete);
                Assert.AreEqual(testTask.Description, response.Description);
                Assert.AreEqual(testTask.StoryId, response.StoryId);
                Assert.AreEqual(testTask.TaskId, response.TaskId);
            });
            Assert.IsInstanceOf<Models.DTOs.Task>(response);
        }       
        
        [Test]
        public void Task_DTOMapping()
        {
            var testTask = new Models.DTOs.Task
            {
                Complete = false,
                Description = "description",
                StoryId = 1,
                TaskId = 2
            };
            var response = Mapper.Map(testTask);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testTask.Complete, response.Complete);
                Assert.AreEqual(testTask.Description, response.Description);
                Assert.AreEqual(testTask.StoryId, response.StoryId);
                Assert.AreEqual(testTask.TaskId, response.TaskId);
            });
            Assert.IsInstanceOf<Models.ServiceModels.Task>(response);
        }

        [Test]
        public void Team_ServiceModelMapping()
        {
            var testTeam = new Models.ServiceModels.Team
            {
                Name = "Team",
                OrganisationId = 1,
                TeamId = 2
            };
            var response = Mapper.Map(testTeam);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testTeam.Name, response.Name);
                Assert.AreEqual(testTeam.TeamId, response.TeamId);
            });
            Assert.IsInstanceOf<Models.DTOs.Team>(response);
        }
        
        [Test]
        public void State_ServiceModelMapping()
        {
            var testState = new Models.ServiceModels.State
            {
                Name = "Team",
                TeamId = 1,
                Type = "test",
                StateId = 2
            };
            var response = Mapper.Map(testState);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testState.Name, response.Name);
                Assert.AreEqual(testState.Type, response.Type);
                Assert.AreEqual(testState.TeamId, response.TeamId);
                Assert.AreEqual(testState.StateId, response.StateId);
            });
            Assert.IsInstanceOf<Models.DTOs.State>(response);
        }
        
        [Test]
        public void Story_ServiceModelMapping()
        {
            var testStory = new Models.ServiceModels.Story
            {
                Archived = false,
                Description = "test description",
                Title = "title",
                CreatedAt = DateTime.Now,
                CreatedBy = 1,
                ListOrder = 2,
                ModifiedAt = DateTime.Now,
                ProjectId = 3,
                SprintId = 4,
                StateId = 5,
                StoryId = 6
            };
            var response = Mapper.Map(testStory);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testStory.Description, response.Description);
                Assert.AreEqual(testStory.Title, response.Title);
                Assert.AreEqual(testStory.ListOrder, response.ListOrder);
                Assert.AreEqual(testStory.ProjectId, response.ProjectId);
                Assert.AreEqual(testStory.SprintId, response.SprintId);
                Assert.AreEqual(testStory.StateId, response.StateId);
                Assert.AreEqual(testStory.StoryId, response.StoryId);
            });
            Assert.IsInstanceOf<Models.DTOs.Story>(response);
        }
        
        [Test]
        public void User_CreateRequestMapping()
        {
            var testUser = new CreateUserRequest
            {
                Email = "test@email.com",
                Name = "test name",
                Password = "password",
                RoleId = 1
            };
            var response = Mapper.Map(testUser);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testUser.Email, response.Email);
                Assert.AreEqual(testUser.Name, response.Name);
                Assert.AreEqual(testUser.RoleId, response.RoleId);
                Assert.AreEqual(0, response.UserId);
            });
            Assert.IsInstanceOf<Models.ServiceModels.User>(response);
        }
        
        [Test]
        public void User_UpdateRequestMapping()
        {
            var testUser = new UpdateUserRequest
            {
                Email = "test@email.com",
                Name = "test name",
                Password = "password",
                RoleId = 1
            };
            var response = Mapper.Map(testUser, new Models.ServiceModels.User
            {
                Email = "t@e.com",
                Name = "t name",
                Password = "actual password",
                RoleId = 0,
                UserId = 2
            });
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testUser.Email, response.Email);
                Assert.AreEqual(testUser.Name, response.Name);
                Assert.AreEqual(testUser.RoleId, response.RoleId);
                Assert.AreEqual(2, response.UserId);
                Assert.AreEqual("actual password", response.Password);
            });
            Assert.IsInstanceOf<Models.ServiceModels.User>(response);
        }
        
        [Test]
        public void Story_CreateRequestMapping()
        {
            var testTasks = new List<Models.DTOs.Task>
            {
                new()
                {
                    Complete = false,
                    Description = "task description",
                    StoryId = 1,
                    TaskId = 2
                }
            };
            var testStory = new CreateStoryRequest
            {
                Description = "test description",
                Title = "title",
                ListOrder = 2,
                ProjectId = 3,
                SprintId = 4,
                StateId = 5,
                Tasks = testTasks
            };
            var response = Mapper.Map(testStory);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testStory.Description, response.Description);
                Assert.AreEqual(testStory.Title, response.Title);
                Assert.AreEqual(testStory.ListOrder, response.ListOrder);
                Assert.AreEqual(testStory.ProjectId, response.ProjectId);
                Assert.AreEqual(testStory.SprintId, response.SprintId);
                Assert.AreEqual(testStory.StateId, response.StateId);
                Assert.AreEqual(0, response.StoryId);
            });
            Assert.IsInstanceOf<Models.ServiceModels.Story>(response);
        }
        
        [Test]
        public void Task_CreateRequestMapping()
        {
            var testTask = new Models.DTOs.Task
            {
                Complete = false,
                Description = "task description",
                StoryId = 0,
                TaskId = 0
            };
            var testTasks = new List<Models.DTOs.Task>
            {
                testTask
            };
            var testStory = new CreateStoryRequest
            {
                Description = "test description",
                Title = "title",
                ListOrder = 2,
                ProjectId = 3,
                SprintId = 4,
                StateId = 5,
                Tasks = testTasks
            };
            var response = Mapper.Map(testStory, 0);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testTasks.Count, response.Count);
                foreach (var task in response)
                {
                    Assert.AreEqual(testTask.Complete, task.Complete);
                    Assert.AreEqual(testTask.Description, task.Description);
                    Assert.AreEqual(testTask.StoryId, task.StoryId);
                    Assert.AreEqual(testTask.TaskId, task.TaskId);
                }
            });
            Assert.IsInstanceOf<List<Models.ServiceModels.Task>>(response);
        }
        
        [Test]
        public void Story_UpdateRequestMapping()
        {
            var testTask = new Models.DTOs.Task
            {
                Complete = false,
                Description = "task description",
                StoryId = 1,
                TaskId = 7
            };
            var testTasks = new List<Models.DTOs.Task>
            {
                testTask
            };
            var testStory = new UpdateStoryRequest
            {
                Description = "test description",
                Title = "title",
                ListOrder = 2,
                ProjectId = 3,
                SprintId = 4,
                StateId = 5,
                StoryId = 6,
                Tasks = testTasks
            };
            var (response, tasks) = Mapper.Map(testStory);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testStory.Description, response.Description);
                Assert.AreEqual(testStory.Title, response.Title);
                Assert.AreEqual(testStory.ListOrder, response.ListOrder);
                Assert.AreEqual(testStory.ProjectId, response.ProjectId);
                Assert.AreEqual(testStory.SprintId, response.SprintId);
                Assert.AreEqual(testStory.StateId, response.StateId);
                Assert.AreEqual(testStory.StoryId, response.StoryId);
                foreach (var task in tasks)
                {
                    Assert.AreEqual(testTask.Complete, task.Complete);
                    Assert.AreEqual(testTask.Description, task.Description);
                    Assert.AreEqual(testTask.StoryId, task.StoryId);
                    Assert.AreEqual(testTask.TaskId, task.TaskId);
                }
            });
            Assert.IsInstanceOf<Models.ServiceModels.Story>(response);
            Assert.IsInstanceOf<List<Models.ServiceModels.Task>>(tasks);
        }
    }
}