using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using WorkTracker.Repositories.Interfaces;
using WorkTracker.Services;

namespace WorkTracker.Test.Services
{
    public class ProjectServiceTest
    {
        private readonly Mock<IProjectRepository> _projectRepository;
        private readonly Mock<ITeamRepository> _teamRepository;
        private readonly Mock<IServiceLogRepository> _serviceLogRepository;
        private readonly ProjectService _projectService;
        public ProjectServiceTest()
        {
            _projectRepository = new Mock<IProjectRepository>();
            _teamRepository = new Mock<ITeamRepository>();
            _serviceLogRepository = new Mock<IServiceLogRepository>();
            _projectService = new ProjectService(_projectRepository.Object,
                _teamRepository.Object,
                _serviceLogRepository.Object);
        }
        
    }
}