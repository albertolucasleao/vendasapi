using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class CreateSaleHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid User entities.
        /// The generated users will have valid:
        /// - IdCustomer (Generates a unique identifier)
        /// - Branch (Generates a unique identifier)
        /// </summary>
        private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
            .RuleFor(s => s.IdCustomer, f => Guid.NewGuid())
            .RuleFor(s => s.Branch, f => Guid.NewGuid());

        private static readonly Faker<CreateSaleProductCommand> createSaleProductHandlerFaker = new Faker<CreateSaleProductCommand>()
            .RuleFor(s => s.IdProduct, f => Guid.NewGuid())
            .RuleFor(s => s.Quantity, f => f.Random.Number(4, 9))
            .RuleFor(s => s.PricesUnit, f => f.Random.Number(1, 100));

        /// <summary>
        /// Generates a valid Sale entity with randomized data.
        /// The generated user will have all properties populated with valid values
        /// that meet the system's validation requirements.
        /// </summary>
        /// <returns>A valid Sale entity with randomly generated data.</returns>
        public static CreateSaleCommand GenerateValidCommand()
        {
            var sale = createSaleHandlerFaker.Generate();
            sale.Products = createSaleProductHandlerFaker.Generate(1);

            return sale;
        }
    }
}
