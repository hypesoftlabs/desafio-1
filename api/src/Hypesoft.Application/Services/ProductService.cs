using Hypesoft.Application.Dtos;
using Hypesoft.Application.Interface;
using Hypesoft.Domain.Entity;
using Hypesoft.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                Category = p.Category,
                StockQuantity = p.StockQuantity


            }).ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(string id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product is null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = product.Category,
                StockQuantity = product.StockQuantity
            };
        }
        public async Task<ProductDto> AddAsync(ProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                StockQuantity = dto.StockQuantity
            };
            await _repository.AddAsync(product);

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = product.Category,
                StockQuantity = product.StockQuantity
            };
        }
        public async Task UpdateAsync(string id, ProductDto dto)
        {
            var product = new Product
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                StockQuantity = dto.StockQuantity
            };
            await _repository.UpdateAsync(product);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
