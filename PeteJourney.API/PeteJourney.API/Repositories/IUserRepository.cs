using Microsoft.IdentityModel.Tokens;
using PeteJourney.API.Models.Domain;

namespace PeteJourney.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
