using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hypesoft.Domain.Entity
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? CategoryId { get; set; }
        public Category? Category { get; set; }
        public int StockQuantity { get; set; }
        public bool IsLowStock => StockQuantity < 10;
        
        public DateTime CreatedAt = DateTime.UtcNow;
        public DateTime UpdatedAt = DateTime.UtcNow;

    }
}
