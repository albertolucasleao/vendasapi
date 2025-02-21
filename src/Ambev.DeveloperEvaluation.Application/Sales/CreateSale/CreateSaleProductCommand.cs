using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleProductCommand
{        
    public Guid IdProduct { get; set;}
    public int Quantity { get; set;}
    public double PricesUnit { get; set;}

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
