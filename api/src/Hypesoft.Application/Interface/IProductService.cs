using Hypesoft.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Interface
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(string id);
        Task<ProductDto> AddAsync(ProductDto dto);
        Task UpdateAsync(string id, ProductDto dto);
        Task DeleteAsync(string id);
    }
}
