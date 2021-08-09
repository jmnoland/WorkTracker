using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.DataModels
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ErrorCode { get; set; }
        public string Request { get; set; }
        public string Stacktrace { get; set; }
        public string FunctionName { get; set; }
        public DateTime? ErrorDate { get; set; }
    }
}
