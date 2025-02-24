namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Represents the response returned after successfully delete a sale.
/// </summary>
/// <remarks>
/// This response contains the status of the sale deletion,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class DeleteSaleResult
{
    /// <summary>
    /// The status of the sale deletion
    /// </summary>
    public bool Success { get; set; }
}
