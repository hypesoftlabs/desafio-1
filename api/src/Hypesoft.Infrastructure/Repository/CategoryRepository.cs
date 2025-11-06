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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _collection;
        public CategoryRepository(MongoDbContext database)
        {
            _collection = database.GetCollection<Category>("Categorys");
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
            
        }

        public async Task<Category?> GetByIdAsync(string id)
        {
            if (id == null)
                return null;
            return await _collection.Find(c => c.Id.ToString() == id).FirstOrDefaultAsync();  
            
        }
        public async Task AddAsync(Category category)
        {
            await _collection.InsertOneAsync(category);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(c => c.Id.ToString() == id);
        }

       
    }
}
