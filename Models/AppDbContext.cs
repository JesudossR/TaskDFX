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
       .ToTable("CLIENT", schema: "FXJPMENRICHDEV");

            modelBuilder.Entity<Team>()
       .ToTable("TEAM", schema: "FXJPMENRICHDEV");

            modelBuilder.Entity<Application>()
       .ToTable("APPLICATION", schema: "FXJPMENRICHDEV");

            modelBuilder.Entity<Environment>()
       .ToTable("ENVIRONMENT", schema: "FXJPMENRICHDEV");

            modelBuilder.Entity<UserRole>()
       .ToTable("USERROLE", schema: "FXJPMENRICHDEV");

            modelBuilder.Entity<ApplicationDetails>()
      .ToTable("APPLICATIONDETAILS", schema: "FXJPMENRICHDEV");

            modelBuilder.Entity<DatabaseDetail>()
        .ToTable("DATABASEDETAIL", schema: "FXJPMENRICHDEV");
        }
    }
}
