using FluentValidation;

namespace Shoppe.Api.Models.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Code).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.ImageUrl).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Quantity).GreaterThanOrEqualTo(0);
        }
    }
}
