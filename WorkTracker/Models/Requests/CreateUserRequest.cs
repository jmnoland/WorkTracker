using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkTracker.Models.Requests
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public List<string> Validate()
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(this.Name)) errors.Add("Name is required");
            if (string.IsNullOrWhiteSpace(this.Email)) errors.Add("Email is required");
            if (string.IsNullOrWhiteSpace(this.Password)) errors.Add("Password is required");
            if (this.Name?.Length > 50) errors.Add("Name must be under 50 characters");
            if (this.Email?.Length > 50) errors.Add("Email must be under 50 characters");
            return errors;
        }
    }
}
