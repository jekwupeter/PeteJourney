using Microsoft.AspNetCore.Mvc;
using PeteJourney.API.Repositories;

namespace PeteJourney.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            // validate the incoming request

            // check if user is authenticated
            var user = await userRepository.AuthenticateAsync(loginRequest.UserName, loginRequest.Password);

            // check username and password
            if (user != null)
            {
                // generate JWT token
               var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest("Incorrect user credentials");
        }
    }
}
