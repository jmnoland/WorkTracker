using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.Requests
{
    public class OrderUpdateRequest
    {
        public int StateId { get; set; }
        public Dictionary<string, int> Stories { get; set; }

        public List<string> Validate()
        {
            var errors = new List<string>();
            if (this.Stories == null || this.Stories.Count == 0)
                errors.Add("Story list is required");
            return errors;
        }
    }
}
