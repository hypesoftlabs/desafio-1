using Hypesoft.Application.Commands.CategoryCommand;
using Hypesoft.Application.Commands.ProductsCommand;
using Hypesoft.Application.Dtos;
using Hypesoft.Application.Interface;
using Hypesoft.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Hypesoft.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        /*private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }*/

        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var query = new GetAllCategoryQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var query = new GetCategoryIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result is null)
                return NotFound(new { message = $"Produto com ID {id} não encontrado." });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
