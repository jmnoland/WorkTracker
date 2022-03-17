using System.Collections.Generic;
using System.Linq;
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
        private readonly ITeamRepository _teamRepository;
        private readonly IServiceLogRepository _serviceLogRepository;

        public ProjectService(IProjectRepository projectRepository,
            ITeamRepository teamRepository,
            IServiceLogRepository serviceLogRepository)
        {
            _projectRepository = projectRepository;
            _teamRepository = teamRepository;
            _serviceLogRepository = serviceLogRepository;
        }

        public async Task<Dictionary<int, List<Project>>> GetByUserId(int userId)
        {
            var teamDict = new Dictionary<int, List<Project>>();
            var teams = await _teamRepository.GetByUserId(userId);
            foreach(var team in teams)
            {
                var projects = await _projectRepository.GetByTeamId(team.TeamId);
                teamDict.Add(team.TeamId, Mapper.Map(projects));
            }
            return teamDict;
        }
    }
}