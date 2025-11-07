using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Commands.ProductsCommand
{
    public class DeleteProductCommand:IRequest<Unit>
    {
        public string Id { get; set; } = string.Empty;
    }
}
