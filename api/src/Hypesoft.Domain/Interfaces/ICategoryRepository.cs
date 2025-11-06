using Hypesoft.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(string id);
        Task AddAsync(Category category);
        Task DeleteAsync(string id);
    }
}
