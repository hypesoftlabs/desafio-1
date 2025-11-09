using Hypesoft.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypesoft.Application.Commands.CategoryCommand
{
    public record CreateCategoryCommand(CategoryDto Category): IRequest<CategoryDto>;
}
