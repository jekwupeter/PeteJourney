using Microsoft.EntityFrameworkCore.Update.Internal;
using PeteJourney.API.Models.Domain;

namespace PeteJourney.API.Repositories
{
    public interface IRunRepository
    {
        Task<IEnumerable<Run>> GetAllAsync();

        Task<Run> GetAsync(Guid id);

        Task<Run> AddAsync(Run run);

        Task<Run> DeleteAsync(Guid Id);

        Task<Run> UpdateAsync(Guid Id, Run run);
    }
}
