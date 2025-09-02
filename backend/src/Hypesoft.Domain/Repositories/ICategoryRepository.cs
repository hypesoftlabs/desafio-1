using Hypesoft.Domain.Entities;

namespace Hypesoft.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
    Task<IEnumerable<Category>> GetActiveCategoriesAsync();
    Task<bool> IsNameUniqueAsync(string name, string? excludeId = null);
}
