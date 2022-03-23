using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkTracker.Models.Requests
{
    public class CreateProjectRequest
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<string> Validate()
        {
            var errors = new List<string>();
            if (this.TeamId == null) errors.Add("TeamId is required");
            if (this.Name?.Length > 100) errors.Add("Name must be under 100 characters");
            if (this.Description?.Length > 750) errors.Add("Description must be under 750 characters");
            return errors;
        }
    }
}