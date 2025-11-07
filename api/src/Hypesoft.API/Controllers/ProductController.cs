using Hypesoft.Application.Dtos;
using Hypesoft.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Hypesoft.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _service.GetByIdAsync(id);
            return product is not null ? Ok(product) : NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto dto)
        {
            var newProduct = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct); 
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Hypesoft.Application.Dtos.ProductDto dto)
        {
            var existingProduct = await _service.GetByIdAsync(id);
            if (existingProduct is null)
            {
                return NotFound();
            }
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var existingProduct = await _service.GetByIdAsync(id);
            if (existingProduct is null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return NoContent();
        }

    }
}
