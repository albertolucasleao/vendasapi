using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTests.GetSale;

// <summary>
/// Contains unit tests for the <see cref="GetSaleCommand"/> class.
/// </summary>
public class GetSaleCommandTests
{
    /// <summary>
    /// Tests that validation passes when all get sale command properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid get sale command data")]
    [Trait("Category", "Sale Test")]
    public void Given_ValidGetSaleCommandData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var command = new GetSaleCommand(Guid.NewGuid());

        // Act
        var result = command.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when get sale command properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid get sale command data")]
    [Trait("Category", "Sale Test")]
    public void Given_InvalidGetSaleCommandData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new GetSaleCommand(Guid.Empty);

        // Act
        var result = command.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
