using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.ServiceModels
{
    public class Story
    {
        public int StoryId { get; set; }
        public int? ProjectId { get; set; }
        public int? SprintId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
