﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.DTOs
{
    public class Task
    {
        public int TaskId { get; set; }
        public int StoryId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
