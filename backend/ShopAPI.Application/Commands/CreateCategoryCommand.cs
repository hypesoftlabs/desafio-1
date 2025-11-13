using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Commands
{
    public class CreateCategoryCommand : IRequest<string>
    {
        public string Name { get; set; }
    }
}
