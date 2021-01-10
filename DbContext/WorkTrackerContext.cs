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
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Task> Task { get; set; }

        public DbSet<UserTeam> UserTeams { get; set; }
        public DbSet<UserTicket> UserTickets { get; set; }
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
            modelBuilder.Entity<Ticket>();
            modelBuilder.Entity<Task>();
            modelBuilder.Entity<Attachment>();
            modelBuilder.Entity<TeamProject>();
            modelBuilder.Entity<UserTeam>();
            modelBuilder.Entity<UserTicket>();
        }
    }
}
