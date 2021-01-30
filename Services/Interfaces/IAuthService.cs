using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Models.Requests;

namespace WorkTracker.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(int userId);
        string RefreshToken(string token);
        bool ValidateCurrentToken(string token);
        bool PermissionAllowed(string token, string permission);
        string Login(UserLoginRequest request);
    }
}
