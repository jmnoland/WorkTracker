using System.Threading.Tasks;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Models.ServiceModels.Role> GetRoleByName(string roleName);
        Task<Models.ServiceModels.Role> GetUserRole(int userId);
    }
}
