using Hypesoft.Application.DTOs;
using MediatR;

namespace Hypesoft.Application.Commands.Products;

public class DeleteProductCommand : IRequest<ApiResponseDto>
{
    public string Id { get; set; } = string.Empty;
}
