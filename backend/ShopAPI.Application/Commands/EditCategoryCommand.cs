using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Commands
{
    public class EditCategoryCommand : IRequest<bool>
    {
      
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
