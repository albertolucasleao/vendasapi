namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

/// <summary>
/// Represents a request to create a new products sale in the system.
/// </summary>
public class CreateSaleProductRequest
{
    /// <summary>
    /// Gets or sets the idproduct. Must contain product identification.
    /// </summary>
    public Guid IdProduct { get; set; }

    /// <summary>
    /// Gets or sets the quantity. Must contain the quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the pricesunit. Must contain the unit value.
    /// </summary>
    public double PricesUnit { get; set; }
}
