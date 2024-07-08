using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodePulse.API.Utility
{
    public class AuthHelpers
    {
        private IConfiguration _configuration;
        private readonly IUserRepository userRepository;

        public AuthHelpers(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            this.userRepository = userRepository;
        }

        public async Task<UserDto?> AuthenticateUser(User login)
        {
            var user = await userRepository.GetUserAsync(login);

            if (user == null)
                return null;

            return new UserDto
            {
                id = login.id,
                username = login.username,
                email = login.email,
                dateOfJoining = login.dateOfJoining
            };
        }

        public string GenerateJWTToken(UserDto user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Email, user.email)
            };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_Secret"])
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
