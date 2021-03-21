using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.Requests
{
    public class UserRegisterRequest
    {
        public int? RoleId { get; set; }
        public int? TeamId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
