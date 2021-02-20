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
        public static Models.ServiceModels.User Map(Models.DataModels.User user)
        {
            return new Models.ServiceModels.User
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                RoleId = user.RoleId,
                UserId = user.UserId
            };
        }
        public static List<Models.DataModels.User> Map(List<Models.ServiceModels.User> userList)
        {
            var temp = new List<Models.DataModels.User>();
            foreach (var user in userList)
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
        #region TeamMapping
        public static Models.ServiceModels.Team Map(Models.DataModels.Team team)
        {
            return new Models.ServiceModels.Team
            {
                Name = team.Name,
                OrganisationId = team.OrganisationId,
                TeamId = team.TeamId
            };
        }
        public static List<Models.ServiceModels.Team> Map(List<Models.DataModels.Team> teamList)
        {
            var tempList = new List<Models.ServiceModels.Team>();
            foreach(var team in teamList)
            {
                tempList.Add(Map(team));
            }
            return tempList;
        }
        #endregion
        #region StateMapping
        public static Models.ServiceModels.State Map(Models.DataModels.State state)
        {
            return new Models.ServiceModels.State
            {
                Name = state.Name,
                StateId = state.StateId,
                TeamId = state.TeamId,
                Type = state.Type
            };
        }
        public static List<Models.ServiceModels.State> Map(List<Models.DataModels.State> stateList)
        {
            var tempList = new List<Models.ServiceModels.State>();
            foreach (var state in stateList)
            {
                tempList.Add(Map(state));
            }
            return tempList;
        }
        #endregion

        #region StoryMapping
        public static Models.ServiceModels.Story Map(Models.DataModels.Story story)
        {
            return new Models.ServiceModels.Story
            {
                StoryId = story.StoryId,
                ProjectId = story.ProjectId,
                SprintId = story.SprintId,
                StateId = story.StateId,
                Title = story.Title,
                Description = story.Description,
            };
        }
        public static List<Models.ServiceModels.Story> Map(List<Models.DataModels.Story> storyList)
        {
            var tempList = new List<Models.ServiceModels.Story>();
            foreach(var story in storyList)
            {
                tempList.Add(Map(story));
            }
            return tempList;
        }
        #endregion
    }
}
