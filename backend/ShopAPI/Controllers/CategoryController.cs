using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Application.Commands;
using ShopAPI.Application.Queries;
using ShopAPI.Domain.Entities;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var query = new GetAllCategoriesQuery();
            var categorias = await _mediator.Send(query);
            return Ok(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var novaCategoriaId = await _mediator.Send(command);

      
            return CreatedAtAction(nameof(GetAllCategories), new { id = novaCategoriaId }, command);
        }
    }
}
