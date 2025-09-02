using Hypesoft.Application.DTOs;
using MediatR;

namespace Hypesoft.Application.Queries.Products;

public class GetProductByIdQuery : IRequest<ApiResponseDto<ProductDto>>
{
    public string Id { get; set; } = string.Empty;
}
