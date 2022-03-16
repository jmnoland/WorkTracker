using System;
using NUnit.Framework;
using WorkTracker.Repositories;

namespace WorkTracker.Test.Mappers
{
    public class RepositoryMapperTest
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
                Assert.AreEqual(testUser.Password, response.Password);
                Assert.AreEqual(testUser.RoleId, response.RoleId);
                Assert.AreEqual(testUser.UserId, response.UserId);
            });
            Assert.IsInstanceOf<Models.DataModels.User>(response);
        }
        
        [Test]
        public void User_DataModelMapping()
        {
            var testUser = new Models.DataModels.User
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
                Assert.AreEqual(testUser.Password, response.Password);
                Assert.AreEqual(testUser.RoleId, response.RoleId);
                Assert.AreEqual(testUser.UserId, response.UserId);
            });
            Assert.IsInstanceOf<Models.ServiceModels.User>(response);
        }
        
        [Test]
        public void Role_DataModelMapping()
        {
            var testRole = new Models.DataModels.Role
            {
                Name = "role name",
                RoleId = 1,
                Permissions = "create,edit"
            };
            var response = Mapper.Map(testRole);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testRole.Name, response.Name);
                Assert.AreEqual(testRole.RoleId, response.RoleId);
                Assert.AreEqual(testRole.Permissions, response.Permissions);
            });
            Assert.IsInstanceOf<Models.ServiceModels.Role>(response);
        }

        [Test]
        public void Team_DataModelMapping()
        {
            var testTeam = new Models.DataModels.Team
            {
                Name = "Team",
                OrganisationId = 1,
                TeamId = 2
            };
            var response = Mapper.Map(testTeam);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testTeam.Name, response.Name);
                Assert.AreEqual(testTeam.OrganisationId, response.OrganisationId);
                Assert.AreEqual(testTeam.TeamId, response.TeamId);
            });
            Assert.IsInstanceOf<Models.ServiceModels.Team>(response);
        }
        
        [Test]
        public void State_DataModelMapping()
        {
            var testState = new Models.DataModels.State
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
            Assert.IsInstanceOf<Models.ServiceModels.State>(response);
        }

        [Test]
        public void Project_DataModelMapping()
        {
            var testProject = new Models.DataModels.Project
            {
                ProjectId = 1,
                Name = "Test name",
                Description = "Test description",
                CompletedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                DeletedAt = DateTime.Now
            };
            var response = Mapper.Map(testProject);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(testProject.ProjectId, response.ProjectId);
                Assert.AreEqual(testProject.Name, response.Name);
                Assert.AreEqual(testProject.Description, response.Description);
                Assert.AreEqual(testProject.CompletedAt, response.CompletedAt);
                Assert.AreEqual(testProject.DeletedAt, response.DeletedAt);
                Assert.AreEqual(testProject.CreatedAt, response.CreatedAt);
            });
            Assert.IsInstanceOf<Models.ServiceModels.Project>(response);
        }
        
        [Test]
        public void Story_DataModelMapping()
        {
            var testStory = new Models.DataModels.Story
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
                Assert.AreEqual(testStory.Archived, response.Archived);
                Assert.AreEqual(testStory.Description, response.Description);
                Assert.AreEqual(testStory.Title, response.Title);
                Assert.AreEqual(testStory.CreatedAt, response.CreatedAt);
                Assert.AreEqual(testStory.CreatedBy, response.CreatedBy);
                Assert.AreEqual(testStory.ListOrder, response.ListOrder);
                Assert.AreEqual(testStory.ModifiedAt, response.ModifiedAt);
                Assert.AreEqual(testStory.ProjectId, response.ProjectId);
                Assert.AreEqual(testStory.SprintId, response.SprintId);
                Assert.AreEqual(testStory.StateId, response.StateId);
                Assert.AreEqual(testStory.StoryId, response.StoryId);
            });
            Assert.IsInstanceOf<Models.ServiceModels.Story>(response);
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
            Assert.IsInstanceOf<Models.DataModels.Story>(response);
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
            Assert.IsInstanceOf<Models.DataModels.Task>(response);
        }
        
        [Test]
        public void Task_DataModelMapping()
        {
            var testTask = new Models.DataModels.Task
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
    }
}