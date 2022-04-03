using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace WorkTracker.Repositories
{
    public static class Mapper
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
        
        public static List<Models.ServiceModels.User> Map(List<Models.DataModels.User> userList)
        {
            var tempList = new List<Models.ServiceModels.User>();
            foreach (var user in userList)
            {
                tempList.Add(Map(user));
            }
            return tempList;
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
                ListOrder = story.ListOrder,
                Description = story.Description,
                Archived = story.Archived,
                CreatedAt = story.CreatedAt,
                CreatedBy = story.CreatedBy,
                ModifiedAt = story.ModifiedAt
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
        public static Models.DataModels.Story Map(Models.ServiceModels.Story story)
        {
            return new Models.DataModels.Story
            {
                StoryId = story.StoryId,
                ProjectId = story.ProjectId,
                SprintId = story.SprintId,
                StateId = story.StateId,
                Title = story.Title,
                ListOrder = story.ListOrder,
                Description = story.Description,
            };
        }
        public static List<Models.DataModels.Story> Map(List<Models.ServiceModels.Story> storyList)
        {
            var tempList = new List<Models.DataModels.Story>();
            foreach (var story in storyList)
            {
                tempList.Add(Map(story));
            }
            return tempList;
        }
        #endregion

        #region ProjectMapping
        public static List<Models.ServiceModels.Project> Map(List<Models.DataModels.Project> itemList)
        {
            var list = new List<Models.ServiceModels.Project>();
            foreach (var item in itemList)
            {
                list.Add(Map(item));
            }
            return list;
        }
        public static Models.ServiceModels.Project Map(Models.DataModels.Project item)
        {
            return new Models.ServiceModels.Project
            {
                ProjectId = item.ProjectId,
                Name = item.Name,
                Description = item.Description,
                CompletedAt = item.CompletedAt,
                CreatedAt = item.CreatedAt,
                DeletedAt = item.DeletedAt
            };
        }
        #endregion

        #region TaskMapping
        public static Models.DataModels.Task Map(Models.ServiceModels.Task task)
        {
            return new Models.DataModels.Task
            {
                Description = task.Description,
                Complete = task.Complete,
                StoryId = task.StoryId,
                TaskId = task.TaskId
            };
        }
        public static List<Models.DataModels.Task> Map(List<Models.ServiceModels.Task> taskList)
        {
            var tempList = new List<Models.DataModels.Task>();
            foreach(var task in taskList)
            {
                tempList.Add(Map(task));
            }
            return tempList;
        }
        public static Models.ServiceModels.Task Map(Models.DataModels.Task task)
        {
            return new Models.ServiceModels.Task
            {
                Complete = task.Complete,
                Description = task.Description,
                StoryId = task.StoryId,
                TaskId = task.TaskId
            };
        }
        public static List<Models.ServiceModels.Task> Map(List<Models.DataModels.Task> taskList)
        {
            var tempList = new List<Models.ServiceModels.Task>();
            foreach(var task in taskList)
            {
                tempList.Add(Map(task));
            }
            return tempList;
        }
        #endregion
    }
}
