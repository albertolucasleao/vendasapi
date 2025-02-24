namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.UpdateSale;

/// <summary>
/// Represents a request to update a sale in the system.
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// Gets or sets the Id. Must contain the identification of the sale.
    /// </summary>
    public Guid Id { get; set; }        
}
