using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> SignupAsync(User user);

        Task<User?> GetUserAsync(User user);
    }
}
