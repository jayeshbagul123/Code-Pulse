using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryRepository _Category;
        public CategoriesController(ICategoryRepository Category)
        {
            _Category = Category;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCatetoryRequestDto request)
        {
            // Map the request DTO to the Category entity
            var Category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await _Category.CreateAsync(Category);

            var response = new CategoryDto
            {
                Id = Category.Id,
                Name = Category.Name,
                UrlHandle = Category.UrlHandle
            };

            return Ok(response);
        }
        [HttpGet("/api/generate-category-no")]

        public async Task<IActionResult> GenerateCategoryNo()
        {
            var number = new Random().Next(1000, 9999);
            return Ok(new { Number = number });
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _Category.GetAllAsync();
            var response = categories.Select(x => new CategoryDto { 
            Id = x.Id,
            Name = x.Name,
            UrlHandle = x.UrlHandle
            });
            return Ok(response);
        }
    }
}
