using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace WorkTracker.Repositories
{
    public class Mapper
    {
        #region UserMapping
        public static Models.ServiceModels.User MapUser(SqlDataReader rdr)
        {
            var user = new Models.ServiceModels.User();
            user.UserId = rdr.GetInt32(0);
            user.RoleId = rdr.GetInt32(1);
            user.Name = rdr.GetString(2);
            user.Email = rdr.GetString(3);
            user.Password = rdr.GetString(4);
            return user;
        }

        public static Models.DataModels.User Map(Models.ServiceModels.User user)
        {
            return new Models.DataModels.User
            {
                Email = user.Email,
                Name = user.Name,
                RoleId = user.RoleId,
                UserId = user.UserId,
                Password = user.Password
            };
        }
        public static List<Models.DataModels.User> Map(List<Models.ServiceModels.User> userList)
        {
            var temp = new List<Models.DataModels.User>();
            foreach(var user in userList)
            {
                temp.Add(Map(user));
            }
            return temp;
        }
        #endregion

        #region RoleMapping
        public static Models.ServiceModels.Role Map(Models.DataModels.Role role)
        {
            return new Models.ServiceModels.Role
            {
                Name = role.Name,
                Permissions = role.Permissions,
                RoleId = role.RoleId
            };
        }
        #endregion
    }
}
