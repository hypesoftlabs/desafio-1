using AutoMapper;
using Hypesoft.Application.DTOs;
using Hypesoft.Application.Interfaces;
using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;

namespace Hypesoft.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseDto<CategoryDto>> CreateCategoryAsync(CreateCategoryDto dto)
    {
        try
        {
            if (await _categoryRepository.IsNameUniqueAsync(dto.Name))
            {
                return ApiResponseDto<CategoryDto>.ErrorResult("Category name already exists");
            }

            var category = _mapper.Map<Category>(dto);
            var createdCategory = await _categoryRepository.CreateAsync(category);
            var categoryDto = _mapper.Map<CategoryDto>(createdCategory);

            return ApiResponseDto<CategoryDto>.SuccessResult(categoryDto, "Category created successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<CategoryDto>.ErrorResult($"Error creating category: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<CategoryDto>> UpdateCategoryAsync(UpdateCategoryDto dto)
    {
        try
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(dto.Id);
            if (existingCategory == null)
            {
                return ApiResponseDto<CategoryDto>.ErrorResult("Category not found");
            }

            if (await _categoryRepository.IsNameUniqueAsync(dto.Name, dto.Id))
            {
                return ApiResponseDto<CategoryDto>.ErrorResult("Category name already exists");
            }

            _mapper.Map(dto, existingCategory);
            var updatedCategory = await _categoryRepository.UpdateAsync(existingCategory);
            var categoryDto = _mapper.Map<CategoryDto>(updatedCategory);

            return ApiResponseDto<CategoryDto>.SuccessResult(categoryDto, "Category updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<CategoryDto>.ErrorResult($"Error updating category: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto> DeleteCategoryAsync(string id)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ApiResponseDto.ErrorResult("Category not found");
            }

            var deleted = await _categoryRepository.DeleteAsync(id);
            if (!deleted)
            {
                return ApiResponseDto.ErrorResult("Failed to delete category");
            }

            return ApiResponseDto.SuccessResult("Category deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto.ErrorResult($"Error deleting category: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<CategoryDto>> GetCategoryByIdAsync(string id)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ApiResponseDto<CategoryDto>.ErrorResult("Category not found");
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return ApiResponseDto<CategoryDto>.SuccessResult(categoryDto);
        }
        catch (Exception ex)
        {
            return ApiResponseDto<CategoryDto>.ErrorResult($"Error retrieving category: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(bool includeInactive = false)
    {
        try
        {
            var categories = includeInactive 
                ? await _categoryRepository.GetAllAsync()
                : await _categoryRepository.GetActiveCategoriesAsync();

            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return ApiResponseDto<IEnumerable<CategoryDto>>.SuccessResult(categoryDtos);
        }
        catch (Exception ex)
        {
            return ApiResponseDto<IEnumerable<CategoryDto>>.ErrorResult($"Error retrieving categories: {ex.Message}");
        }
    }
}
