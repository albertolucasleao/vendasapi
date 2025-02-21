using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleProductValidator : AbstractValidator<CreateSaleProductCommand>
{
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
