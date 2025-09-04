using Hypesoft.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Hypesoft.API.Services;

namespace Hypesoft.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class DashboardController : ControllerBase
  {
  private readonly IMongoCollection<Product> _products;

  public DashboardController(MongoDbService mongoDbService)
  {
    _products = mongoDbService.GetCollection<Product>("Products");
  }

    // GET /api/dashboard/total-products
    [HttpGet("total-products")]
    public ActionResult<object> GetTotalProducts()
    {
      var totalProducts = _products.CountDocuments(_ => true);
      return Ok(new { totalProducts });
    }

    // GET /api/dashboard/total-stock-value
    [HttpGet("total-stock-value")]
    public ActionResult<object> GetTotalStockValue()
    {
      var products = _products.Find(_ => true).ToList();
      var totalValue = products.Sum(p => p.Price * p.Stock);
      return Ok(new { totalStockValue = totalValue });
    }

    // GET /api/dashboard/low-stock
    [HttpGet("low-stock")]
    public ActionResult<object> GetLowStockProducts()
    {
      var filter = Builders<Product>.Filter.Lt(p => p.Stock, 10);
      var lowStock = _products.Find(filter).ToList();
      return Ok(new { lowStockProducts = lowStock });
    }

    // GET /api/dashboard/products-by-category
    [HttpGet("products-by-category")]
    public ActionResult<object> GetProductsByCategory()
    {
      var products = _products.Find(_ => true).ToList();
      var grouped = products
          .GroupBy(p => p.CategoryName)
          .Select(g => new { category = g.Key, count = g.Count() })
          .ToList();

      return Ok(new { productsByCategory = grouped });
    }
  }
}
