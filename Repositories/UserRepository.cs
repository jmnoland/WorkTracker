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
        private readonly WorkTrackerContext _dbContext;

        public UserRepository(WorkTrackerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Models.ServiceModels.User> GetUsers(int teamId)
        {
            string query = @"
                SELECT
                    u.UserId,
                    u.RoleId,
                    u.Name,
                    u.Email,
                    u.Password
                FROM Users u
                LEFT JOIN UserTeams ut ON ut.UserId = u.UserId
                WHERE ut.TeamId = @teamId";

            var conn = (SqlConnection)_dbContext.Database.GetDbConnection();
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@teamId", teamId);
                using (var rdr = cmd.ExecuteReader())
                {
                    var userList = new List<Models.ServiceModels.User>();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            userList.Add(Mapper.MapUser(rdr));
                        }
                    }
                    conn.Close();
                    return userList;
                }
            }
        }

        public User GetUser(int userId)
        {
            return _dbContext.Users.Where(w => w.UserId == userId).FirstOrDefault();
        }

        public async void CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async void UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public void DeleteUser(int userId)
        {
            string query = @"DELETE FROM UserTickets WHERE UserId = @userId
                             DELETE FROM UserTeams WHERE UserId = @userId
                             DELETE FROM Users WHERE UserId = @userId";

            var conn = (SqlConnection)_dbContext.Database.GetDbConnection();
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

    }
}
