using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.UpdateSale;

/// <summary>
/// Validator for UpdateSaleRequest that defines validation rules for sale update.
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Required, must validate the sale identifier.
    /// </remarks>
    public UpdateSaleRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
