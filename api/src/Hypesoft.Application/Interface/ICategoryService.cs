using Hypesoft.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(string id);
        Task<CategoryDto> AddAsync(CategoryDto dto);
        Task DeleteAsync(string id);
    }
}
