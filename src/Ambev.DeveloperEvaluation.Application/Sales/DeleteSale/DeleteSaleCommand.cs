using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Command for delete a sale.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for delete a sale, 
/// including id. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="DeleteSaleResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="DeleteSaleValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class DeleteSaleCommand : IRequest<DeleteSaleResult>
{
    /// <summary>
    /// The unique identifier for the sale
    /// </summary>
    public Guid Id { get; set; }
}
