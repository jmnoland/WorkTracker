using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.DTOs
{
    public class UserDetail
    {
        public string organisation { get; set; }
        public List<State> States { get; set; }
        public List<Team> Teams { get; set; }
        public List<User> Users { get; set; }
    }
}
