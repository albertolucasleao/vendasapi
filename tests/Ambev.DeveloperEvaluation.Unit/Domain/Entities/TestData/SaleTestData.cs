using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated users will have valid:
    /// - DateSale (using current date) 
    /// - DateUpdate (using current date)
    /// - IdCustomer (using unique identifier)
    /// - Branch (branch identifier)
    /// - Status (Sell ​​this asset)
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(u => u.DateSale, f => DateTime.UtcNow)
        .RuleFor(u => u.DateUpdate, f => DateTime.UtcNow)
        .RuleFor(u => u.IdCustomer, f => Guid.NewGuid())
        .RuleFor(u => u.Branch, f => Guid.NewGuid())
        .RuleFor(u => u.Status, f => SaleStatus.Active)
        .RuleFor(u => u.ValueTotal, f => f.Random.Number(10, 100));

    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated users will have valid:
    /// - IdProduct (using unique identifier)
    /// - Quantity (product quantity)
    /// - PricesUnit (using unique identifier)
    /// </summary>
    private static readonly Faker<SaleProduct> SaleProductFaker = new Faker<SaleProduct>()
        .RuleFor(u => u.IdProduct, f => Guid.NewGuid())
        .RuleFor(u => u.Quantity, f => f.Random.Number(1, 20))
        .RuleFor(u => u.PricesUnit, f => f.Random.Number(10, 100));

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static Sale GenerateValidSale()
    {
        var sale = SaleFaker.Generate();
        sale.Products = SaleProductFaker.Generate(1);

        return sale;
    }
}
