using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkTracker.Models.DataModels;
using WorkTracker.Repositories.Interfaces;

namespace WorkTracker.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly WorkTrackerContext _dbContext;

        public RoleRepository(WorkTrackerContext dbContext)
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
    }
}
