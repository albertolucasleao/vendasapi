using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.SaleTestsData.Create;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTests.CreateSale;

// <summary>
/// Contains unit tests for the <see cref="CreateSaleProductCommand"/> class.
/// </summary>
public class CreateSaleProductCommandTests
{
    /// <summary>
    /// Tests that validation passes when all create sale product command properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid create sale product command data")]
    [Trait("Category", "Sale Test")]
    public void Given_ValidCreateSaleProductCommandData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var command = CreateSaleTestData.GenerateCreateSaleProductCommandValid();

        // Act
        var result = command.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when create sale command properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid create sale command data")]
    [Trait("Category", "Sale Test")]
    public void Given_InvalidCreateSaleProductCommandData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var sale = new CreateSaleProductCommand
        {
            IdProduct = Guid.Empty
        };

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
