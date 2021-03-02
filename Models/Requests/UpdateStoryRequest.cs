using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.Requests
{
    public class UpdateStoryRequest
    {
        public int StoryId { get; set; }
        public int StateId { get; set; }
        public int? ProjectId { get; set; }
        public int? SprintId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ListOrder { get; set; }

        public List<DTOs.Task> Tasks { get; set; }
    }
}
