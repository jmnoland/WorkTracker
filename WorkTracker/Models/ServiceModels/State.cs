﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.ServiceModels
{
    public class State
    {
        public int StateId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}