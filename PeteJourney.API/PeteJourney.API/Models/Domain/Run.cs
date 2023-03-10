namespace PeteJourney.API.Models.Domain
{
    public class Run
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid RunDifficultyId { get; set; }
        
        // navigation properties
        public Region Region { get; set; }
        public RunDifficulty RunDifficulty { get; set; }
    }
}
