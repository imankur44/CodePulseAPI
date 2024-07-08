using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostRequestDto requestDto)
        {
            var blogPost = new BlogPost
            {
                Title = requestDto.Title,
                ShortDescription = requestDto.ShortDescription,
                Content = requestDto.Content,
                FeaturedImageUrl = requestDto.FeaturedImageUrl,
                UrlHandle = requestDto.UrlHandle,
                PublishedDate = DateTime.Parse(requestDto.PublishedDate),
                Author = requestDto.Author,
                IsVisible = requestDto.IsVisible
            };

            await blogPostRepository.CreateAsync(blogPost);

            //var response = new BlogPostDto
            //{
            //    Id = blogPost.Id,
            //    Title = blogPost.Title,
            //    ShortDescription = blogPost.ShortDescription,
            //    Content = blogPost.Content,
            //    FeaturedImageUrl = blogPost.FeaturedImageUrl,
            //    UrlHandle = blogPost.UrlHandle,
            //    PublishedDate = blogPost.PublishedDate,
            //    Author = blogPost.Author,
            //    IsVisible = blogPost.IsVisible
            //};

            return Ok(new { success= true,message="Added"});
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdatedBLogPost(BlogPostDto requestDto)
        {
            var blogPost = await blogPostRepository.GetByIdAsync(requestDto.Id);

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible
            };

            await blogPostRepository.SaveAsync(blogPost);

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllBlogPost()
        {
            try
            {
                var blogPost = await blogPostRepository.GetAllAsync();

                List<BlogPost> blogPostDtos = blogPost.Select(blogPost => new BlogPost
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    IsVisible = blogPost.IsVisible
                }).ToList();

                return Ok(blogPostDtos);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<bool> DeleteBlogPostById(Guid id)
        {
            var blogPost = await blogPostRepository.GetByIdAsync(id);

            await blogPostRepository.DeleteAsync(blogPost);

            return true;
        }
    }
}
