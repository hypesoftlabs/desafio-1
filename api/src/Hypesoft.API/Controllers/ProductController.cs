using Hypesoft.Application.Commands.ProductsCommand;
using Hypesoft.Application.Dtos;
using Hypesoft.Application.Interface;
using Hypesoft.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hypesoft.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        /*private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }*/

        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts() {
            var query = new GetAllProductQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var query = new GetProductsIdQuery {Id = id };
            var result = await _mediator.Send(query);

            if (result is null)
                return NotFound(new { message = $"Produto com ID {id} não encontrado." });

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "O ID da URL e do corpo da requisição devem ser iguais." });

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var command = new DeleteProductCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
