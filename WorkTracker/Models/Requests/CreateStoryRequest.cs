using System.Collections.Generic;

namespace WorkTracker.Models.Requests
{
    public class CreateStoryRequest
    {
        public int StateId { get; set; }
        public int? ProjectId { get; set; }
        public int? SprintId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ListOrder { get; set; }
        public List<DTOs.Task> Tasks { get; set; }

        public List<string> Validate()
        {
            var errors = new List<string>();
            if (this.Title == null) errors.Add("Title is required");
            if (this.Title?.Length > 100) errors.Add("Title must be under 100 characters");
            if (this.Description?.Length > 750) errors.Add("Description must be under 750 characters");
            return errors;
        }
    }
}
