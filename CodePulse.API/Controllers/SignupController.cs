using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public SignupController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Signup(CreateUserRequestDto requestDto)
        {
            var user = new User
            {
                id = requestDto.id,
                username = requestDto.username,
                email = requestDto.email,
                password = requestDto.password,
                dateOfJoining = requestDto.dateOfJoining
            };

            await userRepository.SignupAsync(user);

            return Ok(new { success = true, message = "Added" });
        }
    }
}
