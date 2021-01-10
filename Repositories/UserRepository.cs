using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WorkTracker.Models.DataModels;
using WorkTracker.Repositories.Interfaces;

namespace WorkTracker.Repositories
{
    public class UserRepository : IUserRepository
    {
        private WorkTrackerContext _dbContext;

        public UserRepository(WorkTrackerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<User> GetUsers(int teamId)
        {
            string query = @"
                SELECT
                    u.UserId,
                    u.RoleId,
                    u.Name,
                    u.Email,
                    u.Password
                FROM User u
                LEFT JOIN UserTeam ut
                WHERE ut.TeamId = @teamId";
            using (var cmd = new SqlCommand(query, (SqlConnection)_dbContext.Database.GetDbConnection()))
            {
                cmd.Parameters.AddWithValue("@teamId", teamId);
                using (var rdr = cmd.ExecuteReader())
                {
                    var userList = new List<User>();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            userList.Add(Mapper.MapUser(rdr));
                        }
                    }
                    return userList;
                }
            }
        }

    }
}
