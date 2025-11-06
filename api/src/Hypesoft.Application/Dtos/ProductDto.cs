using Hypesoft.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Dtos
{
    public class ProductDto
    {
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? CategoryId { get; set; }
        public Category? Category { get; set; }
        public int StockQuantity { get; set; }
        public bool IsLowStock => StockQuantity < 10;
    }
}
