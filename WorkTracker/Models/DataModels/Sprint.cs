﻿using System;

namespace WorkTracker.Models.DataModels
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
