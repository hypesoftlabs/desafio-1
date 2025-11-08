
using ShopAPI.Application.Common;
using ShopAPI.Domain.Entities;


namespace ShopAPI.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(string id);
        Task<IEnumerable<Product>> GetFullListAsync();
        Task<Pagination<Product>> GetAllAsync(string? nome, string? categoryId, int pageNumber, int pageSize);
        Task<long> GetTotalCountAsync();
        Task<double> GetStorageTotalValueAsync();
        Task<List<Product>> GetLowStorageItemsAsync(int stockLimit);
        Task<bool> HasProductsInTheCategory(string categoryId);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product); 
        Task DeleteAsync(string id); 
    }
}
