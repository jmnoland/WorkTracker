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
                RoleId = (int)request.RoleId,
                UserId = 0
            };
        }
        public static ServiceModels.User Map(UpdateUserRequest request, ServiceModels.User user)
        {
            user.Email = request.Email == user.Email ? user.Email : request.Email;
            user.Name = request.Name == user.Name ? user.Name : request.Name;
            user.RoleId = request.RoleId == user.RoleId ? user.RoleId : (int)request.RoleId;
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

        #region TaskMapping
        public static ServiceModels.Task Map(DTOs.Task task)
        {
            return new ServiceModels.Task
            {
                Description = task.Description,
                Complete = task.Complete,
                StoryId = task.StoryId,
                TaskId = task.TaskId
            };
        }
        public static List<ServiceModels.Task> Map(List<DTOs.Task> taskList)
        {
            var tempList = new List<ServiceModels.Task>();
            foreach (var task in taskList)
            {
                tempList.Add(Map(task));
            }
            return tempList;
        }
        public static DTOs.Task Map(ServiceModels.Task task)
        {
            return new DTOs.Task
            {
                Description = task.Description,
                Complete = task.Complete,
                StoryId = task.StoryId,
                TaskId = task.TaskId
            };
        }
        public static List<DTOs.Task> Map(List<ServiceModels.Task> taskList)
        {
            var tempList = new List<DTOs.Task>();
            foreach (var task in taskList)
            {
                tempList.Add(Map(task));
            }
            return tempList;
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
        #region StoryMapper
        public static DTOs.Story Map(ServiceModels.Story story)
        {
            return new DTOs.Story
            {
                Description = story.Description,
                ProjectId = story.ProjectId,
                SprintId = story.SprintId,
                StateId = story.StateId,
                StoryId = story.StoryId,
                ListOrder = story.ListOrder,
                Title = story.Title,
                Archived = story.Archived,
                CreatedAt = (DateTime)story.CreatedAt,
                CreatedBy = (int)story.CreatedBy,
                ModifiedAt = story.ModifiedAt
            };
        }
        public static List<DTOs.Story> Map(List<ServiceModels.Story> storyList)
        {
            var tempList = new List<DTOs.Story>();
            foreach(var story in storyList)
            {
                tempList.Add(Map(story));
            }
            return tempList;
        }

        // Story create request mapping
        public static ServiceModels.Story Map(CreateStoryRequest request)
        {
            var story = new ServiceModels.Story
            {
                Description = request.Description,
                Title = request.Title,
                ListOrder = request.ListOrder,
                ProjectId = request.ProjectId,
                SprintId = request.SprintId,
                StateId = request.StateId,
                StoryId = 0
            };
            return story;
        }
        public static List<ServiceModels.Task> Map(CreateStoryRequest request, int storyId)
        {
            var taskList = new List<ServiceModels.Task>();
            if (request.Tasks == null) return taskList;
            foreach (var task in request.Tasks)
            {
                taskList.Add(new ServiceModels.Task
                {
                    Description = task.Description,
                    Complete = task.Complete,
                    StoryId = storyId,
                    TaskId = 0
                });
            }
            return taskList;
        }
        public static (ServiceModels.Story, List<ServiceModels.Task>) Map(UpdateStoryRequest request)
        {
            var story = new ServiceModels.Story
            {
                Description = request.Description,
                Title = request.Title,
                ListOrder = request.ListOrder,
                ProjectId = request.ProjectId,
                SprintId = request.SprintId,
                StateId = request.StateId,
                StoryId = request.StoryId
            };
            var taskList = new List<ServiceModels.Task>();
            foreach (var task in request.Tasks)
            {
                taskList.Add(new ServiceModels.Task
                {
                    Description = task.Description,
                    Complete = task.Complete,
                    StoryId = task.StoryId,
                    TaskId = task.TaskId
                });
            }
            return (story, taskList);
        }
        #endregion
    }
}
