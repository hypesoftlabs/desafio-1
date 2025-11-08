using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Application.Commands;
using ShopAPI.Application.Common;
using ShopAPI.Application.DTOs;
using ShopAPI.Application.Queries;
using ShopAPI.Domain.Entities;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPatch("{id}/storage")]
        public async Task<IActionResult> UpdateStorage(
        [FromRoute] string id,
        [FromBody] UpdateStorageCommand command)
        {

            command.ProductId = id;


            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound("Produto não encontrado.");
            }


            return NoContent();
        }


        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetAllProducts(
            [FromQuery] string? name,
            [FromQuery] string? categoryId,
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10  
            )
        {

            var query = new GetAllProductsQuery
            {
                Name = name,
                CategoryId = categoryId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {

            var newProductId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetAllProducts), new { id = newProductId }, command);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(
            [FromRoute] string id,
            [FromBody] EditProductCommand command)
        {

            command.Id = id;


            var result = await _mediator.Send(command);


            if (!result)
            {

                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteProduct([FromRoute] string id)
        {
           
            var command = new DeleteProductCommand(id);

            var result = await _mediator.Send(command);

    
            if (!result)
            {
             
                return NotFound();
            }

            return NoContent();
        }
    }
}
