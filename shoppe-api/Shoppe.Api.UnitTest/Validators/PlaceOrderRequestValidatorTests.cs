using Shoppe.Api.Models;
using Shoppe.Api.Validators;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shoppe.Api.UnitTest.Validators
{
    public class PlaceOrderRequestValidatorTests
    {
        [Theory]
        [InlineData("", "milk", 2.5, 10, 0, false)]
        [InlineData("user-id", "milk", 2.5, 10, 23, false)]
        [InlineData("user-id", "milk", 2.5, 100, 23, true)]
        public void PlaceOrderRequestValidator_Fails_On_Invalid_Input(string userId, string code, float price, int maxAvailable, int qty, bool shouldPass)
        {
            // Arrange
            var product = new ProductSlim(code, price, maxAvailable, qty);
            var request = new PlaceOrderRequest(userId, new List<ProductSlim> { product });

            // Act
            var validator = new PlaceOrderRequestValidator();
            var result = validator.Validate(request);

            // Assert
            Assert.Equal(shouldPass, result.IsValid);

            if (shouldPass)
            {
                Assert.Empty(result.Errors);
            }
            else
            {
                Assert.True(result.Errors.Any());
            }
        }
    }
}
