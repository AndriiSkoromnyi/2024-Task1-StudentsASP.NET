using Xunit;
using Students.Common.ValidationAttributes;

namespace Students.Tests
{
    public class NameValidationTests
    {
        [Theory]
        [InlineData("John Doe")]
        [InlineData("Alice Smith")]
        public void IsValid_ValidNames_ReturnsTrue(string name)
        {
            // Arrange
            var attribute = new NameValidation();

            // Act
            bool isValid = attribute.IsValid(name);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("john doe")]
        [InlineData("John123")]
        [InlineData("JohnDoe1")]
        [InlineData("John_Doe")]
        [InlineData("John-Doe")]
        [InlineData("John   Doe")]
        [InlineData("JohnDoe Smith")]
        [InlineData("John Doe Smith")]
        public void IsValid_InvalidNames_ReturnsFalse(string name)
        {
            // Arrange
            var attribute = new NameValidation();

            // Act
            bool isValid = attribute.IsValid(name);

            // Assert
            Assert.False(isValid);
        }
    }
}
