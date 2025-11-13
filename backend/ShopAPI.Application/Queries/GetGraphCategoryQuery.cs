using MediatR;
using ShopAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Queries
{
    public class GetGraphCategoryQuery : IRequest<IEnumerable<GraphCategoryDTO>>
    {
    }
}
