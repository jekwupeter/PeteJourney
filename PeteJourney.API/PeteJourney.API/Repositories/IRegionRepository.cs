using Microsoft.EntityFrameworkCore.Update.Internal;
using PeteJourney.API.Models.Domain;

namespace PeteJourney.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> GetAsync(Guid id);

        Task<Region> AddAsync(Region region);

        Task<Region> DeleteAsync(Guid Id);

        Task<Region> UpdateAsync(Guid Id, Region region);
    }
}
