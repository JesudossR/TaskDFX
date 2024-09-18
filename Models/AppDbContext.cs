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
            // Client to Team (One-to-Many)
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Teams)
                .WithOne(t => t.Client)
                .HasForeignKey(t => t.ClientID);

            // ApplicationDetails with Application and Client
            //modelBuilder.Entity<ApplicationDetails>()
            //    .HasOne<Application>()
            //    .WithMany()
            //    .HasForeignKey(ad => ad.ApplicationID);

            //modelBuilder.Entity<ApplicationDetails>()
            //    .HasOne<Client>()
            //    .WithMany()
            //    .HasForeignKey(ad => ad.ClientID);

            // Application to ApplicationDetails
            modelBuilder.Entity<Application>()
                .HasMany<ApplicationDetails>()
                .WithOne()
                .HasForeignKey(ad => ad.ApplicationID);

            modelBuilder.Entity<ApplicationDetails>()
               .HasKey(ad => ad.Id);

            // Application to ApplicationDetails (One-to-Many)
            modelBuilder.Entity<Application>()
                .HasMany(a => a.ApplicationDetails)
                .WithOne( ad => ad.Applications)
                .HasForeignKey(ad => ad.ApplicationID);

            // Client to ApplicationDetails (One-to-Many)
            modelBuilder.Entity<Client>()
                .HasMany(c => c.ApplicationDetails)
                .WithOne(ad => ad.Client)
                .HasForeignKey(ad => ad.ClientID);

            // DatabaseDetails is Keyless

            // UserRole is standalone
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => ur.UserID);
        }
    }
}
