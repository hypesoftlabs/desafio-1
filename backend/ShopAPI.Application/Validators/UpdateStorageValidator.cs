using FluentValidation;
using ShopAPI.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.Validators
{
    public class UpdateStorageValidator : AbstractValidator<UpdateStorageCommand>
    {
        public UpdateStorageValidator()
        {
     
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("A quantidade em estoque não pode ser negativa.");
        }
    }
    
}
