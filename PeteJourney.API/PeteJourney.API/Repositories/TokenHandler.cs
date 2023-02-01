using Microsoft.IdentityModel.Tokens;
using PeteJourney.API.Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PeteJourney.API.Repositories
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;

        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<string> CreateTokenAsync(User user)
        {
            // get encoding key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            // Create claims
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            // loop into roles of users
            user.Roles.ForEach((role) =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));  
                });

            // create signing credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // create token using jwtsecuritytoken
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(Int32.Parse(configuration["JwtExpiration"])),
                signingCredentials: credentials);

            // return token using token handler
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
