using Microsoft.EntityFrameworkCore;
using PeteJourney.API.Models;

namespace PeteJourney.API.Data
{
    public class PeteJourneyDbContext: DbContext
    {
        public PeteJourneyDbContext(DbContextOptions<PeteJourneyDbContext> options) : base(options)
        {}

        public DbSet<Region> Regions { get; set; }
        public DbSet<Run> Runs { get; set; }
        public DbSet<RunDifficulty> RunDifficulties { get; set; }

    }
}
