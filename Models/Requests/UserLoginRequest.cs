using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkTracker.Models.Requests
{
    public class UserLoginRequest
    {
        [MaxLength(50)]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
