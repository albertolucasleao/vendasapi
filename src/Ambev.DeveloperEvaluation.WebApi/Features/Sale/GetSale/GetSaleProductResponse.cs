namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;

/// <summary>
/// API response model for GetSale operation
/// </summary>
public class GetSaleProductResponse
{
    /// <summary>
    /// The unique identifier of the selling product
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier for the sale
    /// </summary>
    public Guid IdSale { get; set; }

    /// <summary>
    /// The unique product identifier
    /// </summary>
    public Guid IdProduct { get; set; }

    /// <summary>
    /// The quantity of the product 
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The product unit price
    /// </summary>
    public double PricesUnit { get; set; }

    /// <summary>
    /// The total sale price
    /// </summary>
    public double PricesTotal { get; set; }

    /// <summary>
    /// The discount applied
    /// </summary>
    public double Discount { get; set; }

    /// <summary>
    /// The total with discount
    /// </summary>
    public double TotalPaid { get; set; }
}
