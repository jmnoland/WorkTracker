using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Models.DataModels;
using WorkTracker.Repositories.Interfaces;

namespace WorkTracker.Repositories
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        private readonly WorkTrackerContext _dbContext;

        public TeamRepository(WorkTrackerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async System.Threading.Tasks.Task AssignUser(int userId, int teamId)
        {
            const string query = @"INSERT INTO UserTeams VALUES (@userId, @teamId)";

            var conn = (SqlConnection)_dbContext.Database.GetDbConnection();
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@teamId", teamId);
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
            }
        }

        public async Task<List<Models.ServiceModels.Team>> GetByUserId(int userId)
        {
            var userTeams = await _dbContext.UserTeams
                .Where(w => w.UserId == userId)
                .Select(s => s.TeamId).ToListAsync();
            return Mapper.Map(_dbContext.Teams.Where(w => userTeams.Contains(w.TeamId)).ToList());
        }
    }
}
