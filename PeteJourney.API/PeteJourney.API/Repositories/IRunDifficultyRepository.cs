using Microsoft.EntityFrameworkCore.Update.Internal;
using PeteJourney.API.Models.Domain;

namespace PeteJourney.API.Repositories
{
    public interface IRunDifficultyRepository
    {
        Task<List<RunDifficulty>> GetAllAsync();

        Task<RunDifficulty> GetAsync(Guid id);

        Task<RunDifficulty> AddAsync(RunDifficulty rd);

        Task<RunDifficulty> DeleteAsync(Guid Id);

        Task<RunDifficulty> UpdateAsync(Guid Id, RunDifficulty rd);
    }
}
