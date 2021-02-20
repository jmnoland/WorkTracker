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
            foreach(var user in userList)
            {
                temp.Add(Map(user));
            }
            return temp;
        }

        //  User request mapping
        public static ServiceModels.User Map(CreateUserRequest request)
        {
            return new ServiceModels.User
            {
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
                RoleId = request.RoleId,
                UserId = 0
            };
        }
        public static ServiceModels.User Map(UpdateUserRequest request, ServiceModels.User user)
        {
            user.Email = request.Email == user.Email ? user.Email : request.Email;
            user.Name = request.Name == user.Name ? user.Name : request.Name;
            user.RoleId = request.RoleId == user.RoleId ? user.RoleId : request.RoleId;
            return user;
        }
        #endregion

        #region TeamMapping
        public static DTOs.Team Map(ServiceModels.Team team)
        {
            return new DTOs.Team
            {
                TeamId = team.TeamId,
                Name = team.Name
            };
        }
        public static List<DTOs.Team> Map(List<ServiceModels.Team> teamList)
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
        public static DTOs.State Map(ServiceModels.State state)
        {
            return new DTOs.State
            {
                Name = state.Name,
                Type = state.Type,
                StateId = state.StateId,
                TeamId = state.TeamId
            };
        }
        public static List<DTOs.State> Map(List<ServiceModels.State> stateList)
        {
            var temp = new List<DTOs.State>();
            foreach(var state in stateList)
            {
                temp.Add(Map(state));
            }
            return temp;
        }
        #endregion
    }
}
