namespace Hypesoft.Application.DTOs;

public class DashboardDto
{
    public int TotalProducts { get; set; }
    public decimal TotalStockValue { get; set; }
    public int LowStockProductsCount { get; set; }
    public IEnumerable<LowStockProductDto> LowStockProducts { get; set; } = Enumerable.Empty<LowStockProductDto>();
    public IEnumerable<CategoryStatsDto> CategoryStats { get; set; } = Enumerable.Empty<CategoryStatsDto>();
}

public class LowStockProductDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public int MinimumStock { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}

public class CategoryStatsDto
{
    public string CategoryId { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public int ProductCount { get; set; }
    public decimal TotalValue { get; set; }
}
