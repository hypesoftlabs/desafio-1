using MediatR;
using ShopAPI.Application.Common;
using ShopAPI.Application.DTOs;
using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Queries
{
    public class GetAllProductsQuery : IRequest<Pagination<ProductDTO>>
    {
        public string? Name { get; set; }
        public string? CategoryId { get; set; }
        public int PageNumber { get; set; } = 1; 
        public int PageSize { get; set; } = 10; 
    }
}
