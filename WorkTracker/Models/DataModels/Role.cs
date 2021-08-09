using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.DataModels
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Permissions { get; set; }
    }
}
