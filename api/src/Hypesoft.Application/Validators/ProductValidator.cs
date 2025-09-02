using FluentValidation;
using Hypesoft.Domain.Entities;

namespace Hypesoft.Application.Validators
{
  public class ProductValidator : AbstractValidator<Product>
  {
    public ProductValidator()
    {
      // Name required
      RuleFor(p => p.Name)
          .NotEmpty()
          .WithMessage("Product name is required.");

      // Description required
      RuleFor(p => p.Description)
          .NotEmpty()
          .WithMessage("Product description is required.");

      // Price must be greater than 0
      RuleFor(p => p.Price)
          .GreaterThan(0)
          .WithMessage("Price must be greater than zero.");

      // Stock cannot be negative
      RuleFor(p => p.Stock)
          .GreaterThanOrEqualTo(0)
          .WithMessage("Stock cannot be negative.");

      // CategoryId required and greater than 0
      RuleFor(p => p.CategoryId)
          .GreaterThan(0)
          .WithMessage("CategoryId must be provided.");

      // CategoryName required
      RuleFor(p => p.CategoryName)
          .NotEmpty()
          .WithMessage("CategoryName is required.");
    }
  }
}
