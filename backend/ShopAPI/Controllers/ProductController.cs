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
    public class ProductController : ControllerBase
    {
       
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
           
            var query = new GetAllProductsQuery();

            var products = await _mediator.Send(query);

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CriarProduto([FromBody] CreateProductCommand command)
        {
            
            var newProductId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetAllProducts), new { id = newProductId }, command);
        }
    }
}
