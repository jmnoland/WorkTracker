﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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

        public void AssignUser(int userId, int teamId)
        {
            string query = @"INSERT INTO UserTeams VALUES (@userId, @teamId)";

            var conn = (SqlConnection)_dbContext.Database.GetDbConnection();
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@teamId", teamId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
