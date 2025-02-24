using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.DeleteSale;

/// <summary>
/// Validator for DeleteSaleRequest that defines validation rules for sales deletion.
/// </summary>
public class DeleteSaleRequestValidator : AbstractValidator<DeleteSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Required, must validate the sale identifier.
    /// </remarks>
    public DeleteSaleRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
