using Microsoft.EntityFrameworkCore;
using PeteJourney.API.Data;
using PeteJourney.API.Models.Domain;

namespace PeteJourney.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly PeteJourneyDbContext db;

        public RegionRepository(PeteJourneyDbContext db)
        {
                this.db = db;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await db.Regions.ToListAsync();
        }
    }
}
