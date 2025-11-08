using Microsoft.EntityFrameworkCore;
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Repositories;
using ShopAPI.Infrastructure.Data;

namespace ShopAPI.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository

    {
        private readonly ShopDbContext _context;


        public CategoryRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(string id)
        {

            return await _context.Categories
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task UpdateAsync(Category category)
        {

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(string id)
        {

            var toDelete = await _context.Categories.FindAsync(id);

            if (toDelete != null)
            {
                _context.Categories.Remove(toDelete);
                await _context.SaveChangesAsync();
            }

        }
    }
}
