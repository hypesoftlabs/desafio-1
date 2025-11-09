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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categorys = await _repository.GetAllAsync();
            return categorys.Select(p => new CategoryDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

        public async Task<CategoryDto?> GetByIdAsync(string id)
        {
            var categorys = await _repository.GetByIdAsync(id);
            if (categorys is null) return null;

            return new CategoryDto
            {
                Id = categorys.Id,
                Name = categorys.Name
            };
        }
        public async Task<CategoryDto> AddAsync(CategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };
            await _repository.AddAsync(category);

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

    }
}
