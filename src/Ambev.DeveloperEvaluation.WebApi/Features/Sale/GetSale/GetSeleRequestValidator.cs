using Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;

public class GetSeleRequestValidator : AbstractValidator<GetSaleRequest>
{
    public GetSeleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}
