using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
        RuleFor(s => s.IdCustomer).NotEmpty();
        RuleFor(s => s.Branch).NotEmpty();
        RuleFor(s => s.Product).NotEmpty();
    }
}
