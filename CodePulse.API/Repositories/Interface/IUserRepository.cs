using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> SignupAsync(User user);
    }
}
