using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkTracker.Models.Requests
{
    public class UpdateUserRequest
    {
        public int UserId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
