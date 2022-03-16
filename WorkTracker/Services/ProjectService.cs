using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Models;
using WorkTracker.Models.DTOs;
using WorkTracker.Repositories.Interfaces;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IServiceLogRepository _serviceLogRepository;

        public ProjectService(IProjectRepository projectRepository,
            IServiceLogRepository serviceLogRepository)
        {
            _projectRepository = projectRepository;
            _serviceLogRepository = serviceLogRepository;
        }

        public async Task<List<Project>> GetByTeamId(int teamId)
        {
            var projects = await _projectRepository.GetByTeamId(teamId);
            return Mapper.Map(projects);
        }
    }
}