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

        public IEnumerable<Region> GetAll()
        {
            return db.Regions.ToList();
        }
    }
}
