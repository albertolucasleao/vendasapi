using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for sale creation command.
/// </summary>
public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - IdCustomer: Required, must be in valid format
    /// - Branch: Required, Must contain branch identification
    /// - Products: Required, Must contain the products
    /// </remarks>
    public CreateSaleValidator()
    {
        RuleFor(s => s.IdCustomer).NotEmpty();
        RuleFor(s => s.Branch).NotEmpty();
        RuleForEach(s => s.Products).SetValidator(new CreateSaleProductValidator());
    }
}
