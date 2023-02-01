using PeteJourney.API.Models.Domain;

namespace PeteJourney.API.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
