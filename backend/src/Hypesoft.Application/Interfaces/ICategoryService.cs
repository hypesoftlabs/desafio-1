using Hypesoft.Application.DTOs;

namespace Hypesoft.Application.Interfaces;

public interface ICategoryService
{
    Task<ApiResponseDto<CategoryDto>> CreateCategoryAsync(CreateCategoryDto dto);
    Task<ApiResponseDto<CategoryDto>> UpdateCategoryAsync(UpdateCategoryDto dto);
    Task<ApiResponseDto> DeleteCategoryAsync(string id);
    Task<ApiResponseDto<CategoryDto>> GetCategoryByIdAsync(string id);
    Task<ApiResponseDto<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(bool includeInactive = false);
}
