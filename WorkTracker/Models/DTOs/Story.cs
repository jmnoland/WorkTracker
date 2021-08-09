using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.DTOs
{
    public class Story
    {
        public int StoryId { get; set; }
        public int StateId { get; set; }
        public int? ProjectId { get; set; }
        public int? SprintId { get; set; }
        public int ListOrder { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? Archived { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
