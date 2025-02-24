using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - IdCustomer: Required, must validate the customer identifier
    /// - Branch: Required, must validate branch identifier
    /// </remarks>
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.IdCustomer).NotEmpty();
        RuleFor(x => x.Branch).NotEmpty();
    }
}
