using AutoMapper;
using Hypesoft.Application.DTOs;
using Hypesoft.Application.Interfaces;
using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;

namespace Hypesoft.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseDto<ProductDto>> CreateProductAsync(CreateProductDto dto)
    {
        try
        {
            if (await _productRepository.IsSkuUniqueAsync(dto.Sku))
            {
                return ApiResponseDto<ProductDto>.ErrorResult("Product SKU already exists");
            }

            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                return ApiResponseDto<ProductDto>.ErrorResult("Category not found");
            }

            var product = _mapper.Map<Product>(dto);
            var createdProduct = await _productRepository.CreateAsync(product);
            var productDto = _mapper.Map<ProductDto>(createdProduct);
            productDto.CategoryName = category.Name;

            return ApiResponseDto<ProductDto>.SuccessResult(productDto, "Product created successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<ProductDto>.ErrorResult($"Error creating product: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<ProductDto>> UpdateProductAsync(UpdateProductDto dto)
    {
        try
        {
            var existingProduct = await _productRepository.GetByIdAsync(dto.Id);
            if (existingProduct == null)
            {
                return ApiResponseDto<ProductDto>.ErrorResult("Product not found");
            }

            if (await _productRepository.IsSkuUniqueAsync(dto.Sku, dto.Id))
            {
                return ApiResponseDto<ProductDto>.ErrorResult("Product SKU already exists");
            }

            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                return ApiResponseDto<ProductDto>.ErrorResult("Category not found");
            }

            _mapper.Map(dto, existingProduct);
            var updatedProduct = await _productRepository.UpdateAsync(existingProduct);
            var productDto = _mapper.Map<ProductDto>(updatedProduct);
            productDto.CategoryName = category.Name;

            return ApiResponseDto<ProductDto>.SuccessResult(productDto, "Product updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<ProductDto>.ErrorResult($"Error updating product: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto> DeleteProductAsync(string id)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return ApiResponseDto.ErrorResult("Product not found");
            }

            var deleted = await _productRepository.DeleteAsync(id);
            if (!deleted)
            {
                return ApiResponseDto.ErrorResult("Failed to delete product");
            }

            return ApiResponseDto.SuccessResult("Product deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto.ErrorResult($"Error deleting product: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<ProductDto>> GetProductByIdAsync(string id)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return ApiResponseDto<ProductDto>.ErrorResult("Product not found");
            }

            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            var productDto = _mapper.Map<ProductDto>(product);
            productDto.CategoryName = category?.Name ?? "Unknown";

            return ApiResponseDto<ProductDto>.SuccessResult(productDto);
        }
        catch (Exception ex)
        {
            return ApiResponseDto<ProductDto>.ErrorResult($"Error retrieving product: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<PagedResultDto<ProductDto>>> GetAllProductsAsync(PaginationDto pagination, string? categoryId = null, bool includeInactive = false)
    {
        try
        {
            var products = await _productRepository.GetProductsWithPaginationAsync(pagination.PageNumber, pagination.PageSize);
            var totalCount = await _productRepository.GetTotalProductsCountAsync();

            var productDtos = new List<ProductDto>();
            foreach (var product in products)
            {
                var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
                var productDto = _mapper.Map<ProductDto>(product);
                productDto.CategoryName = category?.Name ?? "Unknown";
                productDtos.Add(productDto);
            }

            var pagedResult = new PagedResultDto<ProductDto>
            {
                Items = productDtos,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

            return ApiResponseDto<PagedResultDto<ProductDto>>.SuccessResult(pagedResult);
        }
        catch (Exception ex)
        {
            return ApiResponseDto<PagedResultDto<ProductDto>>.ErrorResult($"Error retrieving products: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<ProductDto>> UpdateStockAsync(UpdateStockDto dto)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(dto.ProductId);
            if (product == null)
            {
                return ApiResponseDto<ProductDto>.ErrorResult("Product not found");
            }

            product.StockQuantity = dto.Quantity;
            product.UpdatedAt = DateTime.UtcNow;

            var updatedProduct = await _productRepository.UpdateAsync(product);
            var category = await _categoryRepository.GetByIdAsync(updatedProduct.CategoryId);
            var productDto = _mapper.Map<ProductDto>(updatedProduct);
            productDto.CategoryName = category?.Name ?? "Unknown";

            return ApiResponseDto<ProductDto>.SuccessResult(productDto, "Stock updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<ProductDto>.ErrorResult($"Error updating stock: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<IEnumerable<LowStockProductDto>>> GetLowStockProductsAsync(int minimumStock = 10)
    {
        try
        {
            var products = await _productRepository.GetLowStockProductsAsync(minimumStock);
            var lowStockDtos = new List<LowStockProductDto>();

            foreach (var product in products)
            {
                var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
                lowStockDtos.Add(new LowStockProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Sku = product.Sku,
                    StockQuantity = product.StockQuantity,
                    MinimumStock = product.MinimumStock,
                    CategoryName = category?.Name ?? "Unknown"
                });
            }

            return ApiResponseDto<IEnumerable<LowStockProductDto>>.SuccessResult(lowStockDtos);
        }
        catch (Exception ex)
        {
            return ApiResponseDto<IEnumerable<LowStockProductDto>>.ErrorResult($"Error retrieving low stock products: {ex.Message}");
        }
    }
}
