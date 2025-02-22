using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for Sale
/// </summary>
public class SaleValidator : AbstractValidator<Sale>
{
    /// <summary>
    /// Initializes validation rules for Sale
    /// </summary>
    public SaleValidator()
    {
        RuleFor(x => x.DateSale)
            .NotEmpty()
            .WithMessage("DateSale cannot be empty."); ;        
        RuleFor(x => x.IdCustomer)
            .NotEmpty()
            .WithMessage("IdCustomer cannot be empty.");
        RuleFor(x => x.ValueTotal)
            .NotEmpty()
            .WithMessage("ValueTotal cannot be empty.");
        RuleFor(x => x.Branch)
            .NotEmpty()
            .WithMessage("Branch cannot be empty.");
    }
}
