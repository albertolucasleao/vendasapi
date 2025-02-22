using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.UpdateSale;

/// <summary>
/// API response model for UpdateSale operation
/// </summary>
public class UpdateSaleResponse
{
    /// <summary>
    /// The unique identifier for the sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The date of sale
    /// </summary>
    public DateTime DateSale { get; set; }

    /// <summary>
    /// The customer identifier
    /// </summary>
    public Guid IdCustomer { get; set; }

    /// <summary>
    /// The total amount
    /// </summary>
    public double ValueTotal { get; set; }

    /// <summary>
    /// The branch identifier
    /// </summary>
    public Guid Branch { get; set; }

    /// <summary>
    /// The sale is canceled, true or false
    /// </summary>
    public SaleStatus Status { get; set; }

    /// <summary>
    /// The products for sale
    /// </summary>
    public List<UpdateSaleProductResponse> Products { get; set; } = new();
}
