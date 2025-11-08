using MediatR;
using ShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Queries
{
    public class GetAllCategoriesQuery: IRequest<IEnumerable<Category>>
    {
        // Vazia, pois é só a "mensagem"
    }
}
