using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Models.ServiceModels.Role> GetRoleByName(string roleName)
        {
            var role = await _dbContext.Roles.Where(w => w.Name == roleName).FirstOrDefaultAsync();
            if (role != null && role != default)
            {
                return Mapper.Map(role);
            }
            return null;
        }
        public async Task<Models.ServiceModels.Role> GetUserRole(int userId)
        {
            var userRole = await (from user in _dbContext.Users
                        join role in _dbContext.Roles
                            on user.RoleId equals role.RoleId
                        where user.UserId == userId
                        select role).FirstOrDefaultAsync();
            if (userRole == null) return null;
            return Mapper.Map(userRole);
        }
    }
}
