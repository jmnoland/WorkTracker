using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using WorkTracker.Models.ServiceModels;
using WorkTracker.Repositories.Interfaces;
using WorkTracker.Services;

namespace WorkTracker.Test.Services
{
    public class ProjectServiceTest
    {
        private readonly Mock<IProjectRepository> _projectRepository;
        private readonly Mock<ITeamRepository> _teamRepository;
        private readonly ProjectService _projectService;
        private readonly int _demoUserId = 22;
        public ProjectServiceTest()
        {
            _projectRepository = new Mock<IProjectRepository>();
            _teamRepository = new Mock<ITeamRepository>();
            var serviceLogRepository = new Mock<IServiceLogRepository>();
            _projectService = new ProjectService(_projectRepository.Object,
                _teamRepository.Object,
                serviceLogRepository.Object);
        }
        
        [Test]
        public async System.Threading.Tasks.Task GetStoriesByStateId_Successful()
        {
            var now = DateTime.Now;
            var teamRepoResponse = new List<Team>
            {
                new Team
                {
                    Name = "test",
                    OrganisationId = 1,
                    TeamId = 1,
                }
            };
            var expectedProjectResult = new Project
            {
                Name = "test project",
                Description = "test desc",
                CompletedAt = now,
                CreatedAt = now.AddDays(-2),
                DeletedAt = null,
                ProjectId = 0,
                TeamId = 1
            };
            var repoResponse = new List<Project>
            {
                expectedProjectResult
            };
            _teamRepository.Setup(x => x.GetByUserId(_demoUserId))
                .ReturnsAsync(teamRepoResponse);
            _projectRepository.Setup(x => x.GetByTeamId(1))
                .ReturnsAsync(repoResponse);

            var result = await _projectService.GetByUserId(_demoUserId);

            Assert.IsInstanceOf<List<Models.DTOs.Project>>(result);
            Assert.Multiple(() =>
            {
                var project = result.FirstOrDefault();
                Assert.IsTrue(project != null);
                Assert.AreEqual(expectedProjectResult.Name, project.Name);
                Assert.AreEqual(expectedProjectResult.Description, project.Description);
                Assert.AreEqual(expectedProjectResult.CompletedAt, project.CompletedAt);
                Assert.AreEqual(expectedProjectResult.CreatedAt, project.CreatedAt);
                Assert.AreEqual(expectedProjectResult.ProjectId, project.ProjectId);
                Assert.AreEqual(expectedProjectResult.TeamId, project.TeamId);
            });
        }
    }
}