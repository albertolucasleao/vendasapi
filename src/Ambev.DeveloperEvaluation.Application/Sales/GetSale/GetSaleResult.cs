namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class GetSaleResult
{
    /// <summary>
    /// The unique identifier of the user
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
    public bool Cancelled { get; set; }

    /// <summary>
    /// The products for sale
    /// </summary>
    public List<GetSaleProductResult> Products { get; set; } = new();
}
