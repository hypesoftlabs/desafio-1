using FluentValidation;
using ShopAPI.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Validators
{
    public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryCommandValidator()
        {
         
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID da categoria é obrigatório.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
                .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");
        }
    }
}
