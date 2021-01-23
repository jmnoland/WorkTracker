using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Models.ServiceModels.Role GetRoleByName(string roleName);
        Models.ServiceModels.Role GetUserRole(int userId);
    }
}
