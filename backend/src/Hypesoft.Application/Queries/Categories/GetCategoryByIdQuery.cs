using Hypesoft.Application.DTOs;
using MediatR;

namespace Hypesoft.Application.Queries.Categories;

public class GetCategoryByIdQuery : IRequest<ApiResponseDto<CategoryDto>>
{
    public string Id { get; set; } = string.Empty;
}
