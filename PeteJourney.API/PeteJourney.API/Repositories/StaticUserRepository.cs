using PeteJourney.API.Models.Domain;
using System;

namespace PeteJourney.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new()
        {
            new User()
            {
                FirstName = "Read Only", LastName = "User", Email = "readonly@user.com",
                Id = Guid.NewGuid(), Username="readonly@user.com", Password="Readonly@User",
                Roles = new List<string> { "reader"}
            },
            new User()
            {
                FirstName = "Read Write", LastName = "User", Email = "readwrite@user.com",
                Id = Guid.NewGuid() , Username="readwrite@user.com", Password="Readwrite@User",
                Roles = new List<string> { "reader", "writer"}
            }
        };

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = Users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
            x.Password == password);

            return user;
        }
    }
}
