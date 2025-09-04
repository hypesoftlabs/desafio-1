using Hypesoft.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using MongoDB.Driver;

namespace Hypesoft.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
  private readonly IMongoCollection<Product> _products;

    private readonly IValidator<Product> _validator;
    private readonly MongoDB.Driver.IMongoCollection<Hypesoft.Domain.Entities.Category> _categories;

    public ProductsController(IValidator<Product> validator, Hypesoft.API.Services.MongoDbService mongoDbService)
    {
      _validator = validator;
      _categories = mongoDbService.GetCollection<Hypesoft.Domain.Entities.Category>("Categories");
      _products = mongoDbService.GetCollection<Product>("Products");
    }

    // GET /api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAllProducts()
    {
      var products = _products.Find(_ => true).ToList();
      return Ok(products);
    }

    // GET /api/products/{id}
    [HttpGet("{id}")]
    public ActionResult<object> GetProductById(int id)
    {
      var product = _products.Find(p => p.Id == id).FirstOrDefault();
      if (product == null)
        return NotFound(new { message = "Product not found." });

      return Ok(new { message = "Product retrieved successfully.", data = product });
    }

    // POST /api/products
    [HttpPost]
    public ActionResult<object> CreateProduct([FromBody] Product product)
    {
      // Validação básica
      if (string.IsNullOrWhiteSpace(product.Name))
        return BadRequest(new { message = "Product name is required." });

      // Verifica categoria
      var category = _categories.Find(MongoDB.Driver.Builders<Hypesoft.Domain.Entities.Category>.Filter.Eq(c => c.Id, product.CategoryId)).FirstOrDefault();
      if (category == null)
        return BadRequest(new { message = "Invalid category." });

      // Gerar id incremental
      var last = _products.Find(_ => true).SortByDescending(p => p.Id).Limit(1).FirstOrDefault();
      product.Id = last != null ? last.Id + 1 : 1;
      product.CategoryName = category.Name;
      _products.InsertOne(product);

      return CreatedAtAction(nameof(GetProductById),
          new { id = product.Id },
          new { message = "Product created successfully.", data = product });
    }

    // PUT /api/products/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
      var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
      var existingProduct = _products.Find(filter).FirstOrDefault();
      if (existingProduct == null)
        return NotFound(new { message = "Product not found." });

      ValidationResult validationResult = _validator.Validate(updatedProduct);
      if (!validationResult.IsValid)
      {
        return BadRequest(new
        {
          message = "Validation failed.",
          errors = validationResult.Errors.Select(e => e.ErrorMessage)
        });
      }

      var category = _categories.Find(MongoDB.Driver.Builders<Hypesoft.Domain.Entities.Category>.Filter.Eq(c => c.Id, updatedProduct.CategoryId)).FirstOrDefault();
      if (category == null)
        return BadRequest(new { message = "Invalid category." });

      var update = Builders<Product>.Update
        .Set(p => p.Name, updatedProduct.Name)
        .Set(p => p.Description, updatedProduct.Description)
        .Set(p => p.Price, updatedProduct.Price)
        .Set(p => p.Stock, updatedProduct.Stock)
        .Set(p => p.CategoryId, updatedProduct.CategoryId)
        .Set(p => p.CategoryName, category.Name);
      _products.UpdateOne(filter, update);
      var updated = _products.Find(filter).FirstOrDefault();
      return Ok(new { message = "Product updated successfully.", data = updated });
    }

    // DELETE /api/products/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
      var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
      var result = _products.DeleteOne(filter);
      if (result.DeletedCount == 0)
        return NotFound(new { message = "Product not found." });
      return Ok(new { message = "Product deleted successfully." });
    }

    // GET /api/products/search?name=...
    [HttpGet("search")]
    public ActionResult<object> SearchProducts([FromQuery] string name)
    {
      if (string.IsNullOrWhiteSpace(name))
        return BadRequest(new { message = "Search term cannot be empty." });

      var filter = Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));
      var results = _products.Find(filter).ToList();

      if (!results.Any())
        return NotFound(new { message = "No products found matching the search term." });

      return Ok(new { message = "Products retrieved successfully.", data = results });
    }

    // GET /api/products/by-category?category=CategoryName
    [HttpGet("by-category")]
    public ActionResult<object> GetProductsByCategory([FromQuery] string category)
    {
      if (string.IsNullOrWhiteSpace(category))
        return BadRequest(new { message = "Category cannot be empty." });

      var cat = _categories.Find(MongoDB.Driver.Builders<Hypesoft.Domain.Entities.Category>.Filter.Eq(c => c.Name, category)).FirstOrDefault();
      if (cat == null)
        return NotFound(new { message = "Category not found." });

      var results = _products.Find(p => p.CategoryId == cat.Id).ToList();

      if (!results.Any())
        return NotFound(new { message = "No products found for this category." });

      return Ok(new { message = "Products retrieved successfully.", data = results });
    }
  }
}
