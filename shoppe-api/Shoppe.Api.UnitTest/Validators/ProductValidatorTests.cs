using Shoppe.Api.Models;
using Shoppe.Api.Validators;
using System.Linq;
using Xunit;

namespace Shoppe.Api.UnitTest.Validators
{
    public class ProductValidatorTests
    {
        [Theory]
        [InlineData("", "Milk Desc", "image path", 23, 10, 0, false)]
        [InlineData("Milk", "Milk Desc", "image path", -3, 10, 3, false)]
        [InlineData("Milk", "Milk Desc", "image path", 5, 10, -3, false)]
        [InlineData("Milk", "Milk Desc", "image path", 5, 10, 20, false)]
        [InlineData("Milk", "Milk Desc", "image path", 5, 10, 0, true)]
        public void ProductValidator_Fails_On_Invalid_Input(string code, string description, string imageUrl, float price, int maxAvailable, int qty, bool shouldPass = false)
        {
            // Arrange
            var product = new Product(code, description, imageUrl, price, maxAvailable, qty);

            // Act
            var validator = new ProductValidator();
            var result = validator.Validate(product);

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
