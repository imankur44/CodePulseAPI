using CodePulse.API.Data;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            //Map DTO to Domain Model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await categoryRepository.CreatAsync(category);

            //Map Domain model to Dto
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<bool> DeleteCategoryById(Guid id)
        {

            //Map DTO to Domain Model
            var category = await categoryRepository.GetByIdAsync(id);

            await categoryRepository.DeleteAsync(category);

            return true;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                List<Category> allCategories = await categoryRepository.GetAllAsync();

                //Map Domain model to Dto
                List<CategoryDto> categoryDtos = allCategories.Select(category => new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                }).ToList();

                // Return the list of categories as JSON
                return Ok(categoryDtos);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
            {
                Category category = await categoryRepository.GetByIdAsync(id);

                //Map Domain model to Dto
                var response = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                };

                // Return the list of categories as JSON
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
