namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Represents the response returned after successfully creating a new sale.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created sale,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateSaleProductResult
{
    /// <summary>
    /// The unique identifier for the sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The sale identifier
    /// </summary>
    public Guid IdSale { get; set; }

    /// <summary>
    /// The product identifier
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
