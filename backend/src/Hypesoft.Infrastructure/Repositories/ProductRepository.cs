using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using Hypesoft.Infrastructure.Data;
using MongoDB.Driver;

namespace Hypesoft.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MongoDbContext _context;

    public ProductRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.Find(_ => true).ToListAsync();
    }

    public async Task<Product> CreateAsync(Product entity)
    {
        await _context.Products.InsertOneAsync(entity);
        return entity;
    }

    public async Task<Product> UpdateAsync(Product entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.Products.ReplaceOneAsync(p => p.Id == entity.Id, entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _context.Products.DeleteOneAsync(p => p.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var count = await _context.Products.CountDocumentsAsync(p => p.Id == id);
        return count > 0;
    }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(string categoryId)
    {
        return await _context.Products.Find(p => p.CategoryId == categoryId).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int minimumStock = 10)
    {
        return await _context.Products.Find(p => p.StockQuantity <= minimumStock).ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchByNameAsync(string searchTerm)
    {
        var filter = Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"));
        return await _context.Products.Find(filter).ToListAsync();
    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await _context.Products.Find(p => p.Sku == sku).FirstOrDefaultAsync();
    }

    public async Task<bool> IsSkuUniqueAsync(string sku, string? excludeId = null)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Sku, sku);
        
        if (!string.IsNullOrEmpty(excludeId))
        {
            filter = Builders<Product>.Filter.And(
                filter,
                Builders<Product>.Filter.Ne(p => p.Id, excludeId)
            );
        }

        var count = await _context.Products.CountDocumentsAsync(filter);
        return count == 0;
    }

    public async Task<decimal> GetTotalStockValueAsync()
    {
        var pipeline = new[]
        {
            new BsonDocument("$group", new BsonDocument
            {
                { "_id", BsonNull.Value },
                { "totalValue", new BsonDocument("$sum", new BsonDocument("$multiply", new BsonArray { "$price", "$stockQuantity" })) }
            })
        };

        var result = await _context.Products.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();
        return result?["totalValue"]?.AsDecimal ?? 0;
    }

    public async Task<int> GetTotalProductsCountAsync()
    {
        return (int)await _context.Products.CountDocumentsAsync(_ => true);
    }

    public async Task<IEnumerable<Product>> GetProductsWithPaginationAsync(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        return await _context.Products.Find(_ => true)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync();
    }
}
