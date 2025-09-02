using MongoDB.Bson.Serialization.Attributes;

namespace Hypesoft.Domain.Entities;

public class Product : BaseEntity
{
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("price")]
    public decimal Price { get; set; }

    [BsonElement("categoryId")]
    public string CategoryId { get; set; } = string.Empty;

    [BsonElement("stockQuantity")]
    public int StockQuantity { get; set; }

    [BsonElement("minimumStock")]
    public int MinimumStock { get; set; } = 10;

    [BsonElement("isActive")]
    public bool IsActive { get; set; } = true;

    [BsonElement("sku")]
    public string Sku { get; set; } = string.Empty;

    [BsonElement("barcode")]
    public string? Barcode { get; set; }

    [BsonElement("weight")]
    public decimal? Weight { get; set; }

    [BsonElement("dimensions")]
    public ProductDimensions? Dimensions { get; set; }
}

public class ProductDimensions
{
    [BsonElement("length")]
    public decimal Length { get; set; }

    [BsonElement("width")]
    public decimal Width { get; set; }

    [BsonElement("height")]
    public decimal Height { get; set; }

    [BsonElement("unit")]
    public string Unit { get; set; } = "cm";
}
