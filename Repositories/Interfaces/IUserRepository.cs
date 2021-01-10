using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Models.DataModels;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetUsers(int teamId);
    }
}
