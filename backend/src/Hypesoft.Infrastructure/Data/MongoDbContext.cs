using Hypesoft.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Hypesoft.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB") 
            ?? throw new ArgumentNullException(nameof(configuration), "MongoDB connection string is required");
        
        var client = new MongoClient(connectionString);
        var databaseName = configuration["MongoDB:DatabaseName"] ?? "hypesoft";
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Category> Categories => _database.GetCollection<Category>("categories");
    public IMongoCollection<Product> Products => _database.GetCollection<Product>("products");
}
