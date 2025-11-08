using ShopAPI.Domain.Entities;


namespace ShopAPI.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(string id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category categoria);
        Task UpdateAsync(Category categoria); 
        Task DeleteAsync(string id); 
    }
}
