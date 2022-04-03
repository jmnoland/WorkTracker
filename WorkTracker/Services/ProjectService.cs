using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Models;
using WorkTracker.Models.DTOs;
using WorkTracker.Models.Requests;
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

        public async Task<Project> CreateProject(CreateProjectRequest request)
        {
            var project = Mapper.Map(request);
            project.CreatedAt = DateTime.Now;
            var entity = await _projectRepository.CreateProject(project);
            return Mapper.Map(entity, request.TeamId);
        }
        
        public async Task<Project> UpdateProject(UpdateProjectRequest request)
        {
            var project = Mapper.Map(request);
            var entity = await _projectRepository.UpdateProject(project);
            return Mapper.Map(entity, request.TeamId);
        }
        
        public async System.Threading.Tasks.Task DeleteProject(int projectId, int teamId)
        {
            await _projectRepository.DeleteProject(projectId, teamId);
        }

        public async System.Threading.Tasks.Task CompleteProject(int projectId, int teamId)
        {
            await _projectRepository.CompleteProject(projectId, teamId);
        }

        public async Task<List<Project>> GetByUserId(int userId)
        {
            var projectList = new List<Project>();
            var teams = await _teamRepository.GetByUserId(userId);
            foreach(var team in teams)
            {
                var projects = await _projectRepository.GetByTeamId(team.TeamId);
                projectList.AddRange(Mapper.Map(projects, team.TeamId));
            }
            return projectList;
        }
    }
}