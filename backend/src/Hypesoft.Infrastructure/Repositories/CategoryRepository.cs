using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using Hypesoft.Infrastructure.Data;
using MongoDB.Driver;

namespace Hypesoft.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly MongoDbContext _context;

    public CategoryRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<Category?> GetByIdAsync(string id)
    {
        return await _context.Categories.Find(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.Find(_ => true).ToListAsync();
    }

    public async Task<Category> CreateAsync(Category entity)
    {
        await _context.Categories.InsertOneAsync(entity);
        return entity;
    }

    public async Task<Category> UpdateAsync(Category entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.Categories.ReplaceOneAsync(c => c.Id == entity.Id, entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _context.Categories.DeleteOneAsync(c => c.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var count = await _context.Categories.CountDocumentsAsync(c => c.Id == id);
        return count > 0;
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _context.Categories.Find(c => c.Name == name).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Category>> GetActiveCategoriesAsync()
    {
        return await _context.Categories.Find(c => c.IsActive).ToListAsync();
    }

    public async Task<bool> IsNameUniqueAsync(string name, string? excludeId = null)
    {
        var filter = Builders<Category>.Filter.Eq(c => c.Name, name);
        
        if (!string.IsNullOrEmpty(excludeId))
        {
            filter = Builders<Category>.Filter.And(
                filter,
                Builders<Category>.Filter.Ne(c => c.Id, excludeId)
            );
        }

        var count = await _context.Categories.CountDocumentsAsync(filter);
        return count == 0;
    }
}
