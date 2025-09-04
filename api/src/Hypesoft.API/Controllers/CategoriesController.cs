using Microsoft.AspNetCore.Mvc;
using Hypesoft.Domain.Entities;
using System.Collections.Generic;
using MongoDB.Driver;
using Hypesoft.API.Services;

namespace Hypesoft.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CategoriesController : ControllerBase
  {
    private readonly IMongoCollection<Category> _categories;

    public CategoriesController(MongoDbService mongoDbService)
    {
      _categories = mongoDbService.GetCollection<Category>("Categories");
    }

    // GET /api/categories
    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetAll()
    {
      var categories = _categories.Find(_ => true).ToList();
      return Ok(new { message = "Categories retrieved successfully.", data = categories });
    }

    // GET /api/categories/{id}
    [HttpGet("{id}")]
    public ActionResult<Category> Get(int id)
    {
      var category = _categories.Find(c => c.Id == id).FirstOrDefault();
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

      // Gerar Id manualmente (caso não seja gerado pelo MongoDB)
      var last = _categories.Find(_ => true).SortByDescending(c => c.Id).Limit(1).FirstOrDefault();
      category.Id = last != null ? last.Id + 1 : 1;
      _categories.InsertOne(category);

      return CreatedAtAction(nameof(GetAll), new { id = category.Id }, category);
    }

    // PUT /api/categories/{id}
    [HttpPut("{id}")]
    public ActionResult<Category> Update(int id, [FromBody] Category category)
    {
      var filter = Builders<Category>.Filter.Eq(c => c.Id, id);
      var update = Builders<Category>.Update.Set(c => c.Name, category.Name);
      var result = _categories.UpdateOne(filter, update);
      if (result.MatchedCount == 0)
        return NotFound(new { message = "Category not found." });
      var updated = _categories.Find(c => c.Id == id).FirstOrDefault();
      return Ok(updated);
    }

    // DELETE /api/categories/{id}
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      var result = _categories.DeleteOne(c => c.Id == id);
      if (result.DeletedCount == 0)
        return NotFound(new { message = "Category not found." });
      return NoContent();
    }
  }
}
