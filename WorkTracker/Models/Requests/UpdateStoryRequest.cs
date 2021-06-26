using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkTracker.Models.Requests
{
    public class UpdateStoryRequest
    {
        [Required]
        public int StoryId { get; set; }
        [Required]
        public int StateId { get; set; }
        public int? ProjectId { get; set; }
        public int? SprintId { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(750)]
        public string Description { get; set; }
        public int ListOrder { get; set; }

        public List<DTOs.Task> Tasks { get; set; }
    }
}
