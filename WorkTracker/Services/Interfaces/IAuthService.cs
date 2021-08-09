using System.Threading.Tasks;
using WorkTracker.Models.Requests;

namespace WorkTracker.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> CreateToken(int userId);
        Task<string> RefreshToken(string token);
        Task<string> Login(UserLoginRequest request);
        Task Register(UserRegisterRequest request);
        Task<string> DemoLogin();
    }
}
