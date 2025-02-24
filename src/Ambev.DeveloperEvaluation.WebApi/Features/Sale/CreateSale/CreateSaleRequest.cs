namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

/// <summary>
/// Represents a request to create a new sale in the system.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// Gets or sets the idcustomer. Must contain customer identification.
    /// </summary>
    public Guid IdCustomer { get; set; }

    /// <summary>
    /// Gets or sets the username. Must contain branch identification.
    /// </summary>
    public Guid Branch { get; set; }

    /// <summary>
    /// Gets or sets the username. Must contain the products.
    /// </summary>
    public List<CreateSaleProductRequest> Products { get; set; } = new();
}
