using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTracker.Models.DataModels;
using WorkTracker.Repositories.Interfaces;

namespace WorkTracker.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly WorkTrackerContext _dbContext;

        public UserRepository(WorkTrackerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Models.ServiceModels.User>> GetAllUsers(int teamId)
        {
            var team = _dbContext.Teams.FirstOrDefault(w => w.TeamId == teamId);
            var userList = new List<User>();
            if (team == null) return Mapper.Map(userList);
            if (team.OrganisationId != null)
            {
                userList = await (from users in _dbContext.Users
                                  join ut in _dbContext.UserTeams on users.UserId equals ut.UserId
                                  join teams in _dbContext.Teams on ut.TeamId equals teams.TeamId
                                  where teams.OrganisationId == team.OrganisationId
                                  select users).ToListAsync();
            }
            else
            {
                userList = await (from users in _dbContext.Users
                              join ut in _dbContext.UserTeams on users.UserId equals ut.UserId
                              where ut.TeamId == team.TeamId
                              select users).ToListAsync();
            }
            return Mapper.Map(userList);
        }

        public async Task<List<Models.ServiceModels.User>> GetUsersByTeamId(IEnumerable<int> teamIds)
        {
            var userList = await (from users in _dbContext.Users
                              join ut in _dbContext.UserTeams on users.UserId equals ut.UserId
                              where teamIds.Contains(ut.TeamId)
                              select new Models.ServiceModels.User
                              {
                                  Email = users.Email,
                                  Name = users.Name,
                                  RoleId = users.RoleId,
                                  UserId = users.UserId
                              }).ToListAsync();
            return userList;
        }

        public async Task<Models.ServiceModels.User> GetUser(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(w => w.UserId == userId);
            return user == null ? null : Mapper.Map(user);
        }

        public async Task<int> CreateUser(Models.ServiceModels.User user)
        {
            _dbContext.Users.Add(Mapper.Map(user));
            await _dbContext.SaveChangesAsync();
            return user.UserId;
        }

        public async System.Threading.Tasks.Task UpdateUser(Models.ServiceModels.User user)
        {
            _dbContext.Users.Update(Mapper.Map(user));
            await _dbContext.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteUser(int userId)
        {
            const string query = @"DELETE FROM UserStory WHERE UserId = @userId
                             DELETE FROM UserTeams WHERE UserId = @userId
                             DELETE FROM Users WHERE UserId = @userId";

            var conn = (SqlConnection)_dbContext.Database.GetDbConnection();
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@userId", userId);
                await cmd.ExecuteNonQueryAsync();
                await conn.CloseAsync();
            }
        }
    }
}
