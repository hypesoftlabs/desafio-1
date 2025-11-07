using Hypesoft.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Queries
{
    public class GetProductsIdQuery : IRequest<ProductDto>
    {
               public string Id { get; set; } = string.Empty;
    }
}
