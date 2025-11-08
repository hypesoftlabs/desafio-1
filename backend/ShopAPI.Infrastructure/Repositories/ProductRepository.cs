
using Microsoft.EntityFrameworkCore;
using ShopAPI.Application.Common;
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

        public async Task<Pagination<Product>> GetAllAsync(string? name, string? categoryId, int pageNumber, int pageSize)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
            
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            var totalCount = await query.CountAsync();

            var items = await query
            .Skip((pageNumber - 1) * pageSize) 
            .Take(pageSize) 
            .ToListAsync();

            return new Pagination<Product>(items, totalCount, pageNumber, pageSize);
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

        public async Task<Pagination<Product>> GetLowStorageItemsAsync(int stockLimit, int pageNumber, int pageSize)
        {
        
            var query = _context.Products
                              .Where(p => p.Quantity < stockLimit) 
                              .AsQueryable();

       
            var totalCount = await query.CountAsync();

         
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

      
            return new Pagination<Product>(items, totalCount, pageNumber, pageSize);
        }
    }
}