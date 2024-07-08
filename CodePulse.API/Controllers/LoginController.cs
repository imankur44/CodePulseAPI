using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using CodePulse.API.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private  IConfiguration _configuration;
        private readonly IUserRepository userRepository;

        public LoginController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            this.userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            AuthHelpers authHelpers = new AuthHelpers(_configuration, userRepository);
            UserDto? userDto = await authHelpers.AuthenticateUser(login);

            if (userDto == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var tokenString = authHelpers.GenerateJWTToken(userDto);
            return Ok(new { tokenString });
        }
    }
}
