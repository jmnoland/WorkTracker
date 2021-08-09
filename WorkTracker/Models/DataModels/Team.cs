using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.DataModels
{
    public class Team
    {
        public int TeamId { get; set; }
        public int? OrganisationId { get; set; }
        public string Name { get; set; }
    }
}
