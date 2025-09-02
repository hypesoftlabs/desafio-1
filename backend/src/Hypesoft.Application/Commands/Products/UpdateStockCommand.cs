using Hypesoft.Application.DTOs;
using MediatR;

namespace Hypesoft.Application.Commands.Products;

public class UpdateStockCommand : IRequest<ApiResponseDto<ProductDto>>
{
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Reason { get; set; } = string.Empty;
}
