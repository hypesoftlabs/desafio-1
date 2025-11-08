using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Application.Commands;
using ShopAPI.Application.DTOs;
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
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            var query = new GetAllCategoriesQuery();
            var categories = await _mediator.Send(query);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var newCategoryId = await _mediator.Send(command);

      
            return CreatedAtAction(nameof(GetAllCategories), new { id = newCategoryId }, command);
        }

       
        [HttpPut("{id}")] 
        public async Task<IActionResult> EditCategory(
            [FromRoute] string id, 
            [FromBody] EditCategoryCommand command) 
        {
            
            command.Id = id;

         
            var resultado = await _mediator.Send(command);

         
            if (!resultado)
            {
                return NotFound(); 
            }

     
            return NoContent();
        

        }

      
        [HttpDelete("{id}")] 
        public async Task<IActionResult> ExcluirCategoria([FromRoute] string id)
        {
          
            var command = new DeleteCategoryCommand(id);
   
            var resultado = await _mediator.Send(command);

        
            if (!resultado)
            {
         
                return BadRequest("Categoria não encontrada ou está em uso por produtos.");
            }

        
            return NoContent();
        }

       }   
}
