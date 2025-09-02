using Hypesoft.Application.DTOs;
using MediatR;

namespace Hypesoft.Application.Commands.Categories;

public class DeleteCategoryCommand : IRequest<ApiResponseDto>
{
    public string Id { get; set; } = string.Empty;
}
