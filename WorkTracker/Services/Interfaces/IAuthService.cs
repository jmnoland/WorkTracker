using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Models.Requests;

namespace WorkTracker.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateToken(int userId);
        Task<string> RefreshToken(string token);
        bool ValidateCurrentToken(string token);
        bool PermissionAllowed(string token, string permission);
        Task<string> Login(UserLoginRequest request);
        Task Register(UserRegisterRequest request);
        Task<string> DemoLogin();
    }
}
