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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await db.Regions.AddAsync(region);
            await db.SaveChangesAsync();

            return region;
        }

        public async Task<Region> DeleteAsync(Guid Id)
        {
            var regionToDelete = await db.Regions.FirstOrDefaultAsync(x => x.Id == Id);

            if (regionToDelete == null)
            {
                return null;
            }

            db.Regions.Remove(regionToDelete);
            await db.SaveChangesAsync();

            return regionToDelete;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await db.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await db.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid Id, Region region)
        {
            Region regionToUpdate = await db.Regions.FirstOrDefaultAsync(x => x.Id == Id);

            if (regionToUpdate == null)
            {
                return null;
            }

            regionToUpdate.code = region.code;
            regionToUpdate.Area = region.Area;
            regionToUpdate.Name = region.Name;
            regionToUpdate.Lat = region.Lat;
            regionToUpdate.Long = region.Long;
            regionToUpdate.Population = region.Population;

            await db.SaveChangesAsync();

            return regionToUpdate;
        }
    }
}
