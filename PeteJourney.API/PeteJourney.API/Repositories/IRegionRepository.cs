using PeteJourney.API.Models.Domain;

namespace PeteJourney.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
