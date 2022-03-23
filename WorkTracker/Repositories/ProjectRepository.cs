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
                TeamId = project.TeamId,
                ProjectId = project.ProjectId,
                Description = project.Description,
                Name = project.Name,
                CompletedAt = null,
                CreatedAt = project.CreatedAt,
                DeletedAt = null
            };
            _dbContext.Projects.Add(entity);
            await _dbContext.SaveChangesAsync();
            return Mapper.Map(entity);
        }
        
        public async Task<Models.ServiceModels.Project> UpdateProject(Models.ServiceModels.Project project)
        {
            var entity = await _dbContext.Projects
                .FirstOrDefaultAsync(w => w.ProjectId == project.ProjectId
                                          && w.TeamId == project.TeamId
                                          && w.DeletedAt == null);
            if (entity == null) throw new Exception("Update failed project not found");
            entity.Description = project.Description;
            entity.Name = project.Name;
            await _dbContext.SaveChangesAsync();
            return Mapper.Map(entity);
        }
        
        public async Task<Models.ServiceModels.Project> CompleteProject(int projectId, int teamId)
        {
            var entity = await _dbContext.Projects
                .FirstOrDefaultAsync(w => w.ProjectId == projectId
                                          && w.TeamId == teamId
                                          && w.DeletedAt == null);
            if (entity == null) throw new Exception("Completion failed project not found");
            entity.CompletedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return Mapper.Map(entity);
        }
        
        public async Task<Models.ServiceModels.Project> DeleteProject(int projectId, int teamId)
        {
            var entity = await _dbContext.Projects
                .FirstOrDefaultAsync(w => w.ProjectId == projectId
                                          && w.TeamId == teamId
                                          && w.DeletedAt == null);
            if (entity == null) throw new Exception("Deletion failed project not found");
            entity.DeletedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return Mapper.Map(entity);
        }

        public async Task<Project> Find(int projectId, int teamId)
        {
            return await _dbContext.Projects
                .FirstOrDefaultAsync(w => w.ProjectId == projectId
                                          && w.TeamId == teamId
                                          && w.DeletedAt == null);
        }
        
        public async Task<List<Models.ServiceModels.Project>> GetByTeamId(int teamId)
        {
            var data = await _dbContext.Projects
                .Where(w => w.TeamId == teamId && w.DeletedAt == null)
                .ToListAsync();
            return Mapper.Map(data);
        }
    }
}
