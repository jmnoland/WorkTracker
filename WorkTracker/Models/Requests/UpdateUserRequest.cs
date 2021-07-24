using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkTracker.Models.Requests
{
    public class UpdateUserRequest
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }

        public List<string> Validate()
        {
            var errors = new List<string>();
            if (this.UserId == null) errors.Add("UserId is required");
            if (string.Empty == this.Name) errors.Add("Name cannot be empty");
            if (string.Empty == this.Email) errors.Add("Email cannot be empty");
            if (string.Empty == this.Password) errors.Add("Password cannot be empty");
            if (this.Name?.Length > 50) errors.Add("Name must be under 50 characters");
            if (this.Email?.Length > 50) errors.Add("Email must be under 50 characters");
            return errors;
        }
    }
}
