using Xunit;
using Students.Common.ValidationAttributes;

namespace Students.Tests
{
    public class PolishPostalCodeAttributeTests
    {
        [Fact]
        public void IsValid_NullValue_ReturnsTrue()
        {
            // Arrange
            var attribute = new PolishPostalCodeAttribute();

            // Act
            var isValid = attribute.IsValid(null);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("12-345")]
        [InlineData("99-999")]
        public void IsValid_ValidPostalCodes_ReturnsTrue(string postalCode)
        {
            // Arrange
            var attribute = new PolishPostalCodeAttribute();

            // Act
            var isValid = attribute.IsValid(postalCode);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("123-456")]
        [InlineData("AB-123")]
        [InlineData("12-3456")]
        [InlineData("12-3A5")]
        public void IsValid_InvalidPostalCodes_ReturnsFalse(string postalCode)
        {
            // Arrange
            var attribute = new PolishPostalCodeAttribute();

            // Act
            var isValid = attribute.IsValid(postalCode);

            // Assert
            Assert.False(isValid);
        }
    }
}
