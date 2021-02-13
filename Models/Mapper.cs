using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Models.Requests;

namespace WorkTracker.Models.Mapper
{
    public class Mapper
    {
        #region UserMapping
        public static ServiceModels.User Map(DataModels.User user)
        {
            return new ServiceModels.User
            {
                Email = user.Email,
                Name = user.Name,
                RoleId = user.RoleId,
                UserId = user.UserId,
                Password = user.Password
            };
        }
        public static List<ServiceModels.User> Map(List<DataModels.User> userList)
        {
            var temp = new List<ServiceModels.User>();
            foreach(var user in userList)
            {
                temp.Add(Map(user));
            }
            return temp;
        }
        public static DTOs.User Map(ServiceModels.User user)
        {
            return new DTOs.User
            {
                Email = user.Email,
                Name = user.Name,
                RoleId = user.RoleId,
                UserId = user.UserId
            };
        }
        public static List<DTOs.User> Map(List<ServiceModels.User> userList)
        {
            var temp = new List<DTOs.User>();
            foreach (var user in userList)
            {
                temp.Add(Map(user));
            }
            return temp;
        }

        //  User request mapping
        public static DataModels.User Map(CreateUserRequest request)
        {
            return new DataModels.User
            {
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
                RoleId = request.RoleId,
                UserId = 0
            };
        }
        public static DataModels.User Map(UpdateUserRequest request, DataModels.User user)
        {
            user.Email = request.Email == user.Email ? user.Email : request.Email;
            user.Name = request.Name == user.Name ? user.Name : request.Name;
            user.RoleId = request.RoleId == user.RoleId ? user.RoleId : request.RoleId;
            return user;
        }
        #endregion

        #region TeamMapping
        public static DTOs.Team Map(DataModels.Team team)
        {
            return new DTOs.Team
            {
                TeamId = team.TeamId,
                Name = team.Name
            };
        }
        public static List<DTOs.Team> Map(List<DataModels.Team> teamList)
        {
            var temp = new List<DTOs.Team>();
            foreach(var team in teamList)
            {
                temp.Add(new DTOs.Team
                {
                    Name = team.Name,
                    TeamId = team.TeamId
                });
            }
            return temp;
        }
        #endregion

        #region StateMapping
        public static List<DTOs.State> Map(List<DataModels.State> stateList)
        {
            var temp = new List<DTOs.State>();
            foreach(var state in stateList)
            {
                temp.Add(new DTOs.State
                {
                    Name = state.Name,
                    StateId = state.StateId,
                    TeamId = state.TeamId
                });
            }
            return temp;
        }
        public static DTOs.State Map(DataModels.State state)
        {
            return new DTOs.State
            {
                Name = state.Name,
                StateId = state.StateId,
                TeamId = state.TeamId
            };
        }
        #endregion
    }
}
