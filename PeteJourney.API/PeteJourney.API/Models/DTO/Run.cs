
namespace PeteJourney.API.Models.DTO
{
    public class Run
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid RunDifficultyId { get; set; }

        public Region Region { get; set; }
        public RunDifficulty RunDifficulty { get; set; }
    }
}
