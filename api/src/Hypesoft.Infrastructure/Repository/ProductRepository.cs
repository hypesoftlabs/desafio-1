using Hypesoft.Domain.Entity;
using Hypesoft.Domain.Interfaces;
using Hypesoft.Infrastructure.Context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;
       
        public ProductRepository(MongoDbContext database)
        {
            _collection = database.GetCollection<Product>("Products");
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _collection.Find(_=> true).ToListAsync();
            
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            if (id == null)
                return null;
           return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
            
        }
        public async Task AddAsync(Product product)
        {
            await _collection.InsertOneAsync(product);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
            await _collection.ReplaceOneAsync(p => p.Id == product.Id, product);
        }
    }
}
