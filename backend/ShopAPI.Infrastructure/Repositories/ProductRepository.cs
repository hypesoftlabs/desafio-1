
using Microsoft.EntityFrameworkCore; 
using ShopAPI.Domain.Entities;
using ShopAPI.Domain.Repositories;
using ShopAPI.Infrastructure.Data; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopAPI.Infrastructure.Repositories
{
    
    public class ProductRepository : IProductRepository
    {
     
        private readonly ShopDbContext _context;

 
        public ProductRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product Product)
        {
            await _context.Products.AddAsync(Product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
          
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }

 
        public async Task UpdateAsync(Product produto)
        {
          
            _context.Products.Update(produto);
            await _context.SaveChangesAsync();
        }

       
        public async Task DeleteAsync(string id)
        {
           
            var toDelete = await _context.Products.FindAsync(id);
      
            if (toDelete != null)
            {
                _context.Products.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        
        }

}
}