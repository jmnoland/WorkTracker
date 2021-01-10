using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.ViewModels
{
    public class Task
    {
        public int TaskId { get; set; }
        public int TicketId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
