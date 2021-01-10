using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Repositories
{
    public class Mapper
    {
        #region UserMapping
        public static Models.DataModels.User MapUser(SqlDataReader rdr)
        {
            var user = new Models.DataModels.User();
            user.UserId = rdr.GetInt32(0);
            user.RoleId = rdr.GetInt32(1);
            user.Name = rdr.GetString(2);
            user.Email = rdr.GetString(3);
            user.Password = rdr.GetString(4);
            return user;
        }

        public static Models.ServiceModels.User Map(Models.DataModels.User user)
        {
            return new Models.ServiceModels.User
            {
                Email = user.Email,
                Name = user.Name,
                RoleId = user.RoleId,
                UserId = user.UserId,
                Password = user.Password
            };
        }
        public static List<Models.ServiceModels.User> Map(List<Models.DataModels.User> userList)
        {
            var temp = new List<Models.ServiceModels.User>();
            foreach(var user in userList)
            {
                temp.Add(Map(user));
            }
            return temp;
        }
        public static Models.ViewModels.User Map(Models.ServiceModels.User user)
        {
            return new Models.ViewModels.User
            {
                Email = user.Email,
                Name = user.Name,
                RoleId = user.RoleId,
                UserId = user.UserId
            };
        }
        public static List<Models.ViewModels.User> Map(List<Models.ServiceModels.User> userList)
        {
            var temp = new List<Models.ViewModels.User>();
            foreach (var user in userList)
            {
                temp.Add(Map(user));
            }
            return temp;
        }
        #endregion
    }
}
