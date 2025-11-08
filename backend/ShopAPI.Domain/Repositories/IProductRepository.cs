
using ShopAPI.Application.Common;
using ShopAPI.Domain.Entities;


namespace ShopAPI.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(string id);
        Task<Pagination<Product>> GetAllAsync(string? nome, int pageNumber, int pageSize);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product); 
        Task DeleteAsync(string id); 
    }
}
