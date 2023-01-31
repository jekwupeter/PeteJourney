using Microsoft.EntityFrameworkCore;
using PeteJourney.API.Data;
using PeteJourney.API.Models.Domain;

namespace PeteJourney.API.Repositories
{
    public class RunRepository : IRunRepository
    {
        private readonly PeteJourneyDbContext db;

        public RunRepository(PeteJourneyDbContext db)
        {
                this.db = db;
        }

        public async Task<Run> AddAsync(Run run)
        {
            run.Id = Guid.NewGuid();
            await db.Runs.AddAsync(run);
            await db.SaveChangesAsync();

            return run;
        }

        public async Task<Run> DeleteAsync(Guid Id)
        {
            var runToDelete = await db.Runs.FirstOrDefaultAsync(x => x.Id == Id);

            if (runToDelete == null)
            {
                return null;
            }

            db.Runs.Remove(runToDelete);
            await db.SaveChangesAsync();

            return runToDelete;
        }

        public async Task<IEnumerable<Run>> GetAllAsync()
        {
            return await db.Runs
                .Include(x => x.Region)
                .Include(x => x.RunDifficulty)
                .ToListAsync();
        }

        public async Task<Run> GetAsync(Guid id)
        {
            return await db.Runs
                .Include(x => x.RunDifficulty)
                .Include(x => x.Region)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Run> UpdateAsync(Guid Id, Run run)
        {
            Run runToUpdate = await db.Runs.FirstOrDefaultAsync(x => x.Id == Id);

            if (runToUpdate == null)
            {
                return null;
            }

            runToUpdate.Name = run.Name;
            runToUpdate.Length = run.Length;
            runToUpdate.RegionId = run.RegionId;
            runToUpdate.RunDifficultyId = run.RunDifficultyId;

            await db.SaveChangesAsync();

            return runToUpdate;
        }
    }
}
