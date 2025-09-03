using Hypesoft.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Hypesoft.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class DashboardController : ControllerBase
  {
    private static List<Product> Products => ProductsController.Products;

    // GET /api/dashboard/total-products
    [HttpGet("total-products")]
    public ActionResult<object> GetTotalProducts()
    {
      return Ok(new { totalProducts = Products.Count });
    }

    // GET /api/dashboard/total-stock-value
    [HttpGet("total-stock-value")]
    public ActionResult<object> GetTotalStockValue()
    {
      var totalValue = Products.Sum(p => p.Price * p.Stock);
      return Ok(new { totalStockValue = totalValue });
    }

    // GET /api/dashboard/low-stock
    [HttpGet("low-stock")]
    public ActionResult<object> GetLowStockProducts()
    {
      var lowStock = Products.Where(p => p.Stock < 10).ToList();
      return Ok(new { lowStockProducts = lowStock });
    }

    // GET /api/dashboard/products-by-category
    [HttpGet("products-by-category")]
    public ActionResult<object> GetProductsByCategory()
    {
      var grouped = Products
          .GroupBy(p => p.CategoryName)
          .Select(g => new { category = g.Key, count = g.Count() })
          .ToList();

      return Ok(new { productsByCategory = grouped });
    }
  }
}
