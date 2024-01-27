namespace WorkTracker.Models.ServiceModels
{
    public class Team
    {
        public int TeamId { get; set; }
        public int? OrganisationId { get; set; }
        public string Name { get; set; }
    }
}
