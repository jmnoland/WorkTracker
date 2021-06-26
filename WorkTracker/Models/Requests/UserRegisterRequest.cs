using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkTracker.Models.Requests
{
    public class UserRegisterRequest
    {
        public int? RoleId { get; set; }
        public int? TeamId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter the user's first name.")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
