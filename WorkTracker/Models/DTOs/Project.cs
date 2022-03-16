using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.DTOs
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
