using Hypesoft.Application.DTOs;
using MediatR;

namespace Hypesoft.Application.Queries.Products;

public class GetAllProductsQuery : IRequest<ApiResponseDto<PagedResultDto<ProductDto>>>
{
    public PaginationDto Pagination { get; set; } = new();
    public string? CategoryId { get; set; }
    public bool IncludeInactive { get; set; } = false;
}
