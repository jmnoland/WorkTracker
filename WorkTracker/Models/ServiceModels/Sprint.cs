﻿using System;

namespace WorkTracker.Models.ServiceModels
{
    public class Sprint
    {
        public int SprintId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
