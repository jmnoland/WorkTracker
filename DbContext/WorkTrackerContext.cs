using Microsoft.EntityFrameworkCore;

namespace WorkTracker.Models.DataModels
{
    public class WorkTrackerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Story> Story { get; set; }
        public DbSet<Task> Task { get; set; }

        public DbSet<UserTeam> UserTeams { get; set; }
        public DbSet<UserStory> UserStory { get; set; }
        public DbSet<TeamProject> TeamProjects { get; set; }

        private static bool _created = false;

        public WorkTrackerContext(DbContextOptions<WorkTrackerContext> options) 
            : base(options)
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Team>();
            modelBuilder.Entity<Organisation>();
            modelBuilder.Entity<Project>();
            modelBuilder.Entity<Role>();
            modelBuilder.Entity<Sprint>();
            modelBuilder.Entity<State>();
            modelBuilder.Entity<Story>();
            modelBuilder.Entity<Task>();
            modelBuilder.Entity<Attachment>();
            modelBuilder.Entity<TeamProject>().HasKey(e => new { e.TeamId, e.ProjectId });
            modelBuilder.Entity<UserTeam>().HasKey(e => new { e.TeamId, e.UserId });
            modelBuilder.Entity<UserStory>().HasKey(e => new { e.StoryId, e.UserId });
        }
    }
}
