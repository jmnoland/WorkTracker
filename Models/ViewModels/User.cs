using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.ViewModels
{
    public class User
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
