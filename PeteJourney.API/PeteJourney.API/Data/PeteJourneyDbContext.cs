using Microsoft.EntityFrameworkCore;
using PeteJourney.API.Models.Domain;
using System.Diagnostics;

namespace PeteJourney.API.Data
{
    public class PeteJourneyDbContext: DbContext
    {
        public PeteJourneyDbContext(DbContextOptions<PeteJourneyDbContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasOne(u => u.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(y => y.UserId);
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Run> Runs { get; set; }
        public DbSet<RunDifficulty> RunDifficulties { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}
