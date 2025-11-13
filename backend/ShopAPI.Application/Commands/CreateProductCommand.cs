using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Commands
{
    public class CreateProductCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string CategoryId { get; set; }
        public int Quantity { get; set; }
    }
}
