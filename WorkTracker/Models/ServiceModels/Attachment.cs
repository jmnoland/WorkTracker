using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Models.ServiceModels
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public int StoryId { get; set; }
        public string Name { get; set; }
        public string Blob { get; set; }
    }
}
