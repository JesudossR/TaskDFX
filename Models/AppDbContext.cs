using Microsoft.EntityFrameworkCore;

namespace DolphinFx.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Environment> Environments { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ApplicationDetails> ApplicationDetails { get; set; }
        public DbSet<DatabaseDetail> DatabaseDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
       .ToTable("B_CLIENT", schema: "FXJPMENRICHDEV");

            modelBuilder.Entity<Team>()
       .ToTable("B_TEAM", schema: "FXJPMENRICHDEV");

            modelBuilder.Entity<Application>()
       .ToTable("B_APPLICATION", schema: "FXJPMENRICHDEV");

            modelBuilder.Entity<Environment>()
       .ToTable("B_ENVIRONMENT", schema: "FXJPMENRICHDEV");

            modelBuilder.Entity<UserRole>()
       .ToTable("B_USER_ROLE", schema: "FXJPMENRICHDEV");

            modelBuilder.Entity<ApplicationDetails>()
      .ToTable("B_APPLICATION_DETAIL", schema: "FXJPMENRICHDEV");

            modelBuilder.Entity<DatabaseDetail>()
        .ToTable("B_DATABASE_DETAIL", schema: "FXJPMENRICHDEV");
        }
    }
}
