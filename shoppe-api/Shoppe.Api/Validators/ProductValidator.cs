using FluentValidation;
using Shoppe.Api.Models;

namespace Shoppe.Api.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Code).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.ImageUrl).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Quantity)
                .GreaterThanOrEqualTo(0)
                .Must((model, qty) => qty <= model.MaxAvailable); // user should not order more items than available.
        }
    }

    public class ProductsValidator : AbstractValidator<IEnumerable<Product>>
    {
        public ProductsValidator()
        {
            RuleForEach(products => products).NotEmpty().SetValidator(new ProductValidator());
        }
    }
}
