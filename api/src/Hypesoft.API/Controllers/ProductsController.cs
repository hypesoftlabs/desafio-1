using Hypesoft.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Hypesoft.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    public static List<Product> Products = new List<Product>();
    private static int nextId = 1;

    private readonly IValidator<Product> _validator;

    public ProductsController(IValidator<Product> validator)
    {
      _validator = validator;
    }

    // GET /api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAllProducts()
    {
      return Ok(Products);
    }

    // GET /api/products/{id}
    [HttpGet("{id}")]
    public ActionResult<object> GetProductById(int id)
    {
      var product = Products.FirstOrDefault(p => p.Id == id);
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
      var category = CategoriesController.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
      if (category == null)
        return BadRequest(new { message = "Invalid category." });

      product.Id = nextId++;
      product.CategoryName = category.Name;
      Products.Add(product);

      return CreatedAtAction(nameof(GetProductById),
          new { id = product.Id },
          new { message = "Product created successfully.", data = product });
    }

    // PUT /api/products/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
      var existingProduct = Products.FirstOrDefault(p => p.Id == id);
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

      var category = CategoriesController.Categories.FirstOrDefault(c => c.Id == updatedProduct.CategoryId);
      if (category == null)
        return BadRequest(new { message = "Invalid category." });

      existingProduct.Name = updatedProduct.Name;
      existingProduct.Description = updatedProduct.Description;
      existingProduct.Price = updatedProduct.Price;
      existingProduct.Stock = updatedProduct.Stock;
      existingProduct.CategoryId = updatedProduct.CategoryId;
      existingProduct.CategoryName = category.Name;

      return Ok(new { message = "Product updated successfully.", data = existingProduct });
    }

    // DELETE /api/products/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
      var product = Products.FirstOrDefault(p => p.Id == id);
      if (product == null)
        return NotFound(new { message = "Product not found." });

      Products.Remove(product);
      return Ok(new { message = "Product deleted successfully.", data = product });
    }

    // GET /api/products/search?name=...
    [HttpGet("search")]
    public ActionResult<object> SearchProducts([FromQuery] string name)
    {
      if (string.IsNullOrWhiteSpace(name))
        return BadRequest(new { message = "Search term cannot be empty." });

      var results = Products
          .Where(p => p.Name.ToLower().Contains(name.ToLower()))
          .ToList();

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

      var cat = CategoriesController.Categories
          .FirstOrDefault(c => c.Name.ToLower() == category.ToLower());
      if (cat == null)
        return NotFound(new { message = "Category not found." });

      var results = Products.Where(p => p.CategoryId == cat.Id).ToList();

      if (!results.Any())
        return NotFound(new { message = "No products found for this category." });

      return Ok(new { message = "Products retrieved successfully.", data = results });
    }
  }
}
