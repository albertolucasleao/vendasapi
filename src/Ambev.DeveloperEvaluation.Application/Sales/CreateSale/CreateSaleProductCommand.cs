using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new sale product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a sale product, 
/// including idproduct, quantity, pricesunit. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateSaleProductResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateSaleProductValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateSaleProductCommand
{
    /// <summary>
    /// Gets or sets the idproduct of the sale to be created.
    /// </summary>
    public Guid IdProduct { get; set;}

    /// <summary>
    /// Gets or sets the quantity of the sale to be created.
    /// </summary>
    public int Quantity { get; set;}

    /// <summary>
    /// Gets or sets the pricesunit of the sale to be created.
    /// </summary>
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
