using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTests.UpdateSale;

// <summary>
/// Contains unit tests for the <see cref="UpdateSaleCommand"/> class.
/// </summary>
public class UpdateSaleCommandTests
{
    /// <summary>
    /// Tests that validation passes when all update sale command properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid update sale command data")]
    [Trait("Category", "Sale Test")]
    public void Given_ValidUpdateSaleCommandData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var command = new UpdateSaleCommand { Id = Guid.NewGuid() };

        // Act
        var result = command.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when update sale command properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid update sale command data")]
    [Trait("Category", "Sale Test")]
    public void Given_InvalidUpdateSaleCommandData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new UpdateSaleCommand { Id = Guid.Empty };

        // Act
        var result = command.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
