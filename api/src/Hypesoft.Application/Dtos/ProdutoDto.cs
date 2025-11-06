using Hypesoft.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Dtos
{
    public class ProdutoDto
    {
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int CategoryId { get; private set; }
        public Category? Category { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsLowStock => StockQuantity < 10;
    }
}
