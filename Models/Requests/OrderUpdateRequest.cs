using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.Requests
{
    public class OrderUpdateRequest
    {
        public int StateId { get; set; }
        public Dictionary<string, int> Stories { get; set; }
    }
}
