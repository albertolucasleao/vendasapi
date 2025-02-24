using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTests.DeleteSale;

// <summary>
/// Contains unit tests for the <see cref="DeleteSaleCommand"/> class.
/// </summary>
public class DeleteSaleCommandTests
{
    /// <summary>
    /// Tests that validation passes when all delete sale command properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid delete sale command data")]
    [Trait("Category", "Sale Test")]
    public void Given_ValidDeleteSaleCommandData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var command = new DeleteSaleCommand { Id = Guid.NewGuid() };

        // Act
        var result = command.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when delete sale command properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid delete sale command data")]
    [Trait("Category", "Sale Test")]
    public void Given_InvalidDeleteSaleCommandData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new DeleteSaleCommand { Id = Guid.Empty };

        // Act
        var result = command.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
