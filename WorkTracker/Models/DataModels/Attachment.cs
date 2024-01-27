namespace WorkTracker.Models.DataModels
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public int StoryId { get; set; }
        public string Name { get; set; }
        public string Blob { get; set; }
    }
}
