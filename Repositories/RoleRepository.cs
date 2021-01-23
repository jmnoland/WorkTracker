using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkTracker.Models.DataModels;
using WorkTracker.Repositories.Interfaces;

namespace WorkTracker.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly WorkTrackerContext _dbContext;

        public RoleRepository(WorkTrackerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Models.ServiceModels.Role GetRoleByName(string roleName)
        {
            var role = _dbContext.Roles.Where(w => w.Name == roleName).FirstOrDefault();
            if (role != null && role != default)
            {
                return Mapper.Map(role);
            }
            return null;
        }
        public Models.ServiceModels.Role GetUserRole(int userId)
        {
            var userRole = (from user in _dbContext.Users
                        join role in _dbContext.Roles 
                            on user.RoleId equals role.RoleId
                        where user.UserId == userId
                        select Mapper.Map(role)).FirstOrDefault();
            return userRole;
        }
    }
}
