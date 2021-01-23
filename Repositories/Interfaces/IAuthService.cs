using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(int userId);
    }
}
