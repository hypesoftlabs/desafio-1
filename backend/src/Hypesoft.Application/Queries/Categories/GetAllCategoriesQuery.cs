using Hypesoft.Application.DTOs;
using MediatR;

namespace Hypesoft.Application.Queries.Categories;

public class GetAllCategoriesQuery : IRequest<ApiResponseDto<IEnumerable<CategoryDto>>>
{
    public bool IncludeInactive { get; set; } = false;
}
