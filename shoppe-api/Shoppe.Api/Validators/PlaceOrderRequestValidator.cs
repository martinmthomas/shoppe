using FluentValidation;
using Shoppe.Api.Models;

namespace Shoppe.Api.Validators
{
    public class PlaceOrderRequestValidator : AbstractValidator<PlaceOrderRequest>
    {
        public PlaceOrderRequestValidator()
        {
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.Products).SetValidator(new ProductSlimsValidator());
        }
    }
}
