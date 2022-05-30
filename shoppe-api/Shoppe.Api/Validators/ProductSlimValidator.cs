using FluentValidation;
using Shoppe.Api.Models;

namespace Shoppe.Api.Validators
{
    public class ProductSlimValidator : AbstractValidator<ProductSlim>
    {
        public ProductSlimValidator()
        {
            RuleFor(p => p.Code).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Quantity)
                .GreaterThanOrEqualTo(0)
                .Must((model, qty) => qty <= model.MaxAvailable); // user should not order more items than available.
        }
    }

    public class ProductSlimsValidator : AbstractValidator<IEnumerable<ProductSlim>>
    {
        public ProductSlimsValidator()
        {
            RuleForEach(products => products).NotEmpty().SetValidator(new ProductSlimValidator());
        }
    }
}
