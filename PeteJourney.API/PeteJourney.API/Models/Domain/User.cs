using System.ComponentModel.DataAnnotations.Schema;

namespace PeteJourney.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; }

        // naviagation propety
        public List<UserRole> UserRoles { get; set; }

    }
}
