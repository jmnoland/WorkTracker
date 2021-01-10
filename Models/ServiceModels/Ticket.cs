using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.ServiceModels
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int ProjectId { get; set; }
        public int SprintId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
