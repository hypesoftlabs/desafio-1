using Microsoft.AspNetCore.Mvc;
using Hypesoft.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Hypesoft.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CategoriesController : ControllerBase
  {
    // Lista em memória
    public static List<Category> Categories = new List<Category>();
    private static int nextCategoryId = 1;

    // GET /api/categories
    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetAll()
    {
      return Ok(new { message = "Categories retrieved successfully.", data = Categories });
    }

    // GET /api/categories/{id}
    [HttpGet("{id}")]
    public ActionResult<Category> Get(int id)
    {
      var category = Categories.FirstOrDefault(c => c.Id == id);
      if (category == null)
        return NotFound(new { message = "Category not found." });
      return category;
    }

    // POST /api/categories
    [HttpPost]
    public ActionResult<Category> Create([FromBody] Category category)
    {
      if (string.IsNullOrWhiteSpace(category.Name))
        return BadRequest(new { message = "Category name is required." });

      category.Id = nextCategoryId++;
      Categories.Add(category);

      return CreatedAtAction(nameof(GetAll), new { id = category.Id }, category);
    }

    // PUT /api/categories/{id}
    [HttpPut("{id}")]
    public ActionResult<Category> Update(int id, [FromBody] Category category)
    {
      var existing = Categories.FirstOrDefault(c => c.Id == id);
      if (existing == null) return NotFound(new { message = "Category not found." });

      existing.Name = category.Name;
      return Ok(existing);
    }

    // DELETE /api/categories/{id}
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      var existing = Categories.FirstOrDefault(c => c.Id == id);
      if (existing == null) return NotFound(new { message = "Category not found." });

      Categories.Remove(existing);
      return NoContent();
    }
  }
}
