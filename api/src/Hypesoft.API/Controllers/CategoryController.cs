using Hypesoft.Application.Dtos;
using Hypesoft.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Hypesoft.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllCategories() => Ok(await _service.GetAllAsync());
        
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var category = await _service.GetByIdAsync(id);
            return category is not null ? Ok(category) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto dto)
        {
            var createdCategory = await _service.AddAsync(dto); 
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var existingCategory = await _service.GetByIdAsync(id);
            if (existingCategory is null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
