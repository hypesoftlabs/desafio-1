using Hypesoft.Domain.Entities;

namespace Hypesoft.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryIdAsync(string categoryId);
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int minimumStock = 10);
    Task<IEnumerable<Product>> SearchByNameAsync(string searchTerm);
    Task<Product?> GetBySkuAsync(string sku);
    Task<bool> IsSkuUniqueAsync(string sku, string? excludeId = null);
    Task<decimal> GetTotalStockValueAsync();
    Task<int> GetTotalProductsCountAsync();
    Task<IEnumerable<Product>> GetProductsWithPaginationAsync(int page, int pageSize);
}
