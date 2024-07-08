using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreatAsync(Category category);
        Task<bool> DeleteAsync(Category category);
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(Guid id);
    }
}
