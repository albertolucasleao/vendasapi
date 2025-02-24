using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.SaleTestsData.Entities;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleTestData
{
    /// <summary>
    /// Configures the Faker to generate valid sale entities.
    /// The generated sales will have valid:
    /// - DateSale (Generates valid date)
    /// - DateUpdate (Generates valid date)
    /// - IdCustomer (Generates a unique identifier)
    /// - ValueTotal (Generates valid value)
    /// - Branch (Generates branch identifier)
    /// - Status (Generate random status)
    /// </summary>
    private static readonly Faker<Sale> createSaleFaker = new Faker<Sale>()
        .RuleFor(s => s.DateSale, f => DateTime.UtcNow)
        .RuleFor(s => s.DateUpdate, f => DateTime.UtcNow)
        .RuleFor(s => s.IdCustomer, f => Guid.NewGuid())
        .RuleFor(s => s.ValueTotal, f => f.Random.Number(10, 100))
        .RuleFor(s => s.Branch, f => Guid.NewGuid())
        .RuleFor(s => s.Status, f => f.PickRandom(SaleStatus.Active, SaleStatus.Canceled));

    /// <summary>
    /// Configures the Faker to generate valid sale product entities.
    /// The generated sales will have valid:
    /// - IdProduct (Generates a unique identifier)
    /// - Quantity (Generates random quantity, from 1 to 20)
    /// - PricesUnit (Generates random value)
    /// - Discount (Generates a unique identifier)
    /// - TotalPaid (Generates a unique identifier)
    /// - PricesTotal (Generates a unique identifier)
    /// </summary>
    private static readonly Faker<SaleProduct> createSaleProductFaker = new Faker<SaleProduct>()
        .RuleFor(s => s.IdProduct, f => Guid.NewGuid())
        .RuleFor(s => s.Quantity, f => f.Random.Number(1, 20))
        .RuleFor(s => s.PricesUnit, f => f.Random.Number(10, 100))
        .RuleFor(s => s.Discount, f => 0.1)
        .RuleFor(s => s.TotalPaid, (f, s) => s.Quantity * s.PricesUnit * (1 - s.Discount))
        .RuleFor(s => s.PricesTotal, (f, s) => s.Quantity * s.PricesUnit);

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated sale will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateSaleValid()
    {
        var sale = createSaleFaker.Generate();
        sale.Products = createSaleProductFaker.Generate(1);

        return sale;
    }
}
