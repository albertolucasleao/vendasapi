using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleCommand
/// </summary>
public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes validation rules for UpdateSaleCommand
    /// </summary>
    public UpdateSaleValidator()
    {
        RuleFor(s => s.Id).NotEmpty();
    }
}
