using FluentValidation;
using Shoppe.Api.Models;

namespace Shoppe.Api.Validators
{
    public class CartUpdateRequestValidator : AbstractValidator<CartUpdateRequest>
    {
        public CartUpdateRequestValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.Products).SetValidator(new ProductSlimsValidator());
        }
    }
}
