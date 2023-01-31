using Microsoft.EntityFrameworkCore;
using PeteJourney.API.Data;
using PeteJourney.API.Models.Domain;

namespace PeteJourney.API.Repositories
{
    public class RunDifficultyRepository : IRunDifficultyRepository
    {
        private readonly PeteJourneyDbContext db;

        public RunDifficultyRepository(PeteJourneyDbContext db)
        {
                this.db = db;
        }

        public async Task<RunDifficulty> AddAsync(RunDifficulty rd)
        {
            rd.Id = Guid.NewGuid();
            await db.RunDifficulties.AddAsync(rd);
            await db.SaveChangesAsync();

            return rd;
        }

        public async Task<RunDifficulty> DeleteAsync(Guid Id)
        {
            var rdToDelete = await db.RunDifficulties.FirstOrDefaultAsync(x => x.Id == Id);

            if (rdToDelete == null)
            {
                return null;
            }

            db.RunDifficulties.Remove(rdToDelete);
            await db.SaveChangesAsync();

            return rdToDelete;
        }

        public async Task<List<RunDifficulty>> GetAllAsync()
        {
            return await db.RunDifficulties.ToListAsync();
        }

        public async Task<RunDifficulty> GetAsync(Guid id)
        {
            return await db.RunDifficulties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RunDifficulty> UpdateAsync(Guid Id, RunDifficulty rd)
        {
            RunDifficulty rdToUpdate = await db.RunDifficulties.FirstOrDefaultAsync(x => x.Id == Id);

            if (rdToUpdate == null)
            {
                return null;
            }

            rdToUpdate.Code = rd.Code;
            

            await db.SaveChangesAsync();

            return rdToUpdate;
        }
    }
}
