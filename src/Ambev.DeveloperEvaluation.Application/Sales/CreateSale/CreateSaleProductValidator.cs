using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleProductCommand that defines validation rules for sale product creation command.
/// </summary>
public class CreateSaleProductValidator : AbstractValidator<CreateSaleProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleProductValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - IdProduct: Required, Must be in valid format 
    /// - Quantity: Required, must contain the quantity of products
    /// - PricesUnit: Required, must contain the unit price of the product
    /// </remarks>
    public CreateSaleProductValidator()
    {
        RuleFor(s => s.IdProduct).NotEmpty();
        RuleFor(s => s.Quantity).NotEmpty()
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(20)
            .WithMessage("A quantidade deve ser de no mínimo 1 e máximo 20 itens.");
        RuleFor(s => s.PricesUnit).NotEmpty();
    }
}
