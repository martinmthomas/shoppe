using Shoppe.Api.Models;
using Shoppe.Api.Models.Validators;
using System.Linq;
using Xunit;

namespace Shoppe.Api.UnitTest.Validators
{
    public class ProductValidatorTests
    {
        [Theory]
        [InlineData("", "Milk Desc", "image path", 23, 0)]
        [InlineData("Milk", "Milk Desc", "image path", -3, 3)]
        [InlineData("Milk", "Milk Desc", "image path", 5, -3)]
        [InlineData("Milk", "Milk Desc", "image path", 5, 0, true)]
        public void ProductValidator_Fails_On_Invalid_Input(string code, string description, string imageUrl, float price, int qty, bool shouldPass = false)
        {
            // Arrange
            var product = new Product(code, description, imageUrl, price, qty);

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
