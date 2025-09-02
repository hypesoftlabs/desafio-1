using Hypesoft.Application.DTOs;
using MediatR;

namespace Hypesoft.Application.Commands.Products;

public class CreateProductCommand : IRequest<ApiResponseDto<ProductDto>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CategoryId { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public int MinimumStock { get; set; } = 10;
    public string Sku { get; set; } = string.Empty;
    public string? Barcode { get; set; }
    public decimal? Weight { get; set; }
    public ProductDimensionsDto? Dimensions { get; set; }
}
