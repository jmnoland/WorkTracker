using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.DTOs
{
    public class State
    {
        public int StateId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
    }
}
