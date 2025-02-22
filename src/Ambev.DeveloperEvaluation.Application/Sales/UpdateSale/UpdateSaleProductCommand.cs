namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command for update a sale
/// </summary>
public class UpdateSaleProductCommand
{
    /// <summary>
    /// The unique identifier of the sale to delete
    /// </summary>
    public Guid Id { get; set; }

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
    /// The discount applied
    /// </summary>
    public string Discount { get; set; } = string.Empty;
}
