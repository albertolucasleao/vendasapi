namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale;

/// <summary>
/// Represents a request to delete a sale in the system.
/// </summary>
public class DeleteSaleRequest
{
    /// <summary>
    /// Gets or sets the Id. Must contain sale identification.
    /// </summary>
    public Guid Id { get; set; }
}
