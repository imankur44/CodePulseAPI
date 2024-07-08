using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<bool> DeleteAsync(BlogPost blogPost)
        {
            dbContext.BlogPosts.Remove(blogPost);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetByIdAsync(Guid id)
        {
            return await dbContext.BlogPosts.FindAsync(id);
        }

        public async Task<bool> SaveAsync(BlogPost blogPost)
        {
            dbContext.BlogPosts.Update(blogPost);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
