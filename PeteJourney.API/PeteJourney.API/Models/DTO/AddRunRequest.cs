namespace PeteJourney.API.Models.DTO
{
    public class AddRunRequest
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid RunDifficultyId { get; set; }
    }
}
