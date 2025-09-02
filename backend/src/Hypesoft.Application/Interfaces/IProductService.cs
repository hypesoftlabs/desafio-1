using Hypesoft.Application.DTOs;

namespace Hypesoft.Application.Interfaces;

public interface IProductService
{
    Task<ApiResponseDto<ProductDto>> CreateProductAsync(CreateProductDto dto);
    Task<ApiResponseDto<ProductDto>> UpdateProductAsync(UpdateProductDto dto);
    Task<ApiResponseDto> DeleteProductAsync(string id);
    Task<ApiResponseDto<ProductDto>> GetProductByIdAsync(string id);
    Task<ApiResponseDto<PagedResultDto<ProductDto>>> GetAllProductsAsync(PaginationDto pagination, string? categoryId = null, bool includeInactive = false);
    Task<ApiResponseDto<ProductDto>> UpdateStockAsync(UpdateStockDto dto);
    Task<ApiResponseDto<IEnumerable<LowStockProductDto>>> GetLowStockProductsAsync(int minimumStock = 10);
}
