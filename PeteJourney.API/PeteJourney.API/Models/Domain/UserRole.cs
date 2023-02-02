namespace PeteJourney.API.Models.Domain
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        
        // navaigation property
        public  User User{ get; set; }
        public Role Role { get; set; }
    }
}
