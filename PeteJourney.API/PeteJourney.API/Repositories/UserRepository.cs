using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using PeteJourney.API.Data;
using PeteJourney.API.Models.Domain;

namespace PeteJourney.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PeteJourneyDbContext db;

        public UserRepository(PeteJourneyDbContext db)
        {
            this.db = db;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            // get user
            var user = db.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower() && u.Password == u.Password);

            if (user == null)
            {
                return null;
            }

            // get user roles
            var userRoles = await db.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();

            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                // add role names to user
                foreach (var userRole in userRoles)
                {
                    var role = await db.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null)
                    { 
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.Password = null;

            return user;
        }
    }
}
