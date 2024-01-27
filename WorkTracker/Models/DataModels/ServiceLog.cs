using System;

namespace WorkTracker.Models.DataModels
{
    public class ServiceLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TeamId { get; set; }
        public int? CountAffected { get; set; }
        public string FunctionName { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
