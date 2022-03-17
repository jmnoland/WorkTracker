using System.Collections.Generic;
using System.Threading.Tasks;
using WorkTracker.Models.DTOs;

namespace WorkTracker.Services.Interfaces
{
    public interface IProjectService
    {
        Task<Dictionary<int, List<Project>>> GetByUserId(int userId);
    }
}