using PeteJourney.API.Models.Domain;

namespace PeteJourney.API.Repositories
{
    public interface IRegionRepository
    {
        IEnumerable<Region> GetAll();
    }
}
