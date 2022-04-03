using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkTracker.Models.DataModels;
using WorkTracker.Repositories.Interfaces;

namespace WorkTracker.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly WorkTrackerContext _dbContext;

        public ProjectRepository(WorkTrackerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Models.ServiceModels.Project> CreateProject(Models.ServiceModels.Project project)
        {
            var entity = new Project
            {
                ProjectId = project.ProjectId,
                Description = project.Description,
                Name = project.Name,
                CompletedAt = null,
                CreatedAt = project.CreatedAt,
                DeletedAt = null
            };
            _dbContext.Projects.Add(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.TeamProjects.Add(new TeamProject
            {
                ProjectId = entity.ProjectId,
                TeamId = project.TeamId,
            });
            await _dbContext.SaveChangesAsync();
            return Mapper.Map(entity);
        }
        
        public async Task<Models.ServiceModels.Project> UpdateProject(Models.ServiceModels.Project project)
        {
            var entity = await (from proj in _dbContext.Projects
                join teamProj in _dbContext.TeamProjects
                    on proj.ProjectId equals teamProj.ProjectId
                where proj.ProjectId == project.ProjectId
                      && teamProj.TeamId == project.TeamId
                      && proj.DeletedAt == null
                select proj)
                .FirstOrDefaultAsync();
            if (entity == null) throw new Exception("Update failed project not found");
            entity.Description = project.Description;
            entity.Name = project.Name;
            await _dbContext.SaveChangesAsync();
            return Mapper.Map(entity);
        }
        
        public async Task<Models.ServiceModels.Project> CompleteProject(int projectId, int teamId)
        {
            var entity = await (from proj in _dbContext.Projects
                    join teamProj in _dbContext.TeamProjects
                        on proj.ProjectId equals teamProj.ProjectId
                    where proj.ProjectId == projectId
                          && teamProj.TeamId == teamId
                          && proj.DeletedAt == null
                    select proj)
                .FirstOrDefaultAsync();
            if (entity == null) throw new Exception("Completion failed project not found");
            entity.CompletedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return Mapper.Map(entity);
        }
        
        public async Task<Models.ServiceModels.Project> DeleteProject(int projectId, int teamId)
        {
            var entity = await (from proj in _dbContext.Projects
                    join teamProj in _dbContext.TeamProjects
                        on proj.ProjectId equals teamProj.ProjectId
                    where proj.ProjectId == projectId
                          && teamProj.TeamId == teamId
                          && proj.DeletedAt == null
                    select proj)
                .FirstOrDefaultAsync();
            if (entity == null) throw new Exception("Deletion failed project not found");
            entity.DeletedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return Mapper.Map(entity);
        }

        public async Task<Models.ServiceModels.Project> Find(int projectId, int teamId)
        {
            return await (from project in _dbContext.Projects
                join teamProj in _dbContext.TeamProjects
                    on project.ProjectId equals teamProj.ProjectId
                where teamProj.ProjectId == projectId
                      && teamProj.TeamId == teamId
                      && project.DeletedAt == null
                select Mapper.Map(project))
                .FirstOrDefaultAsync();
        }
        
        public async Task<List<Models.ServiceModels.Project>> GetByTeamId(int teamId)
        {
            var data = await (from project in _dbContext.Projects
                join teamProj in _dbContext.TeamProjects
                    on project.ProjectId equals teamProj.ProjectId
                where teamProj.TeamId == teamId && project.DeletedAt == null
                select Mapper.Map(project)).ToListAsync();
            return data;
        }
    }
}
