using Hypesoft.Application.DTOs;
using MediatR;

namespace Hypesoft.Application.Queries.Products;

public class GetLowStockProductsQuery : IRequest<ApiResponseDto<IEnumerable<LowStockProductDto>>>
{
    public int MinimumStock { get; set; } = 10;
}
